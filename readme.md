# Beerpong Simulator—Dokumentation

## Spielprinzip

Beim Beerpong Simulator wird das beliebte Trinkspiel in Virtual Reality simuliert. Meistens wird Beerpong auf einem Tisch, bzw. in unserem Fall einer Tischtennisplatte, gespielt. An diesem Tisch stehen sich zwei Spieler gegenüber und haben vor sich jeweils zehn Plastikbecher aufgereiht, die zur Hälfte mit Bier gefüllt sind. Abwechselnd wird versucht mit einem Tischtennisball in einen Becher des Gegners zu treffen. Bei einem Treffer muss der Gegner den getroffenen Becher austrinken. Gewonnen hat der Spieler, der zuerst alle Becher des Gegners getroffen hat.


## Technisches Setup

Umgesetzt wurde das Spiel in Unity. Als Endgerät haben wir uns für ein Android-Gerät und die Google Daydream entschieden, weil dies wesentlich flexibler als andere VR-Geräte (z.B. Oculus Rift oder HTC Vive) ist. Das liegt vor allem daran, dass die Daydream nicht mit Kabeln an einem Rechner verbunden werden muss, sondern lediglich eine Brille ist, durch die auf das Smartphone geschaut wird. Zudem bietet Google eine fertige Unity SDK an, die die Entwicklung bzw. das Builden der Android-App stark vereinfacht.
Für das initiale Setup und das finale Testing haben wir uns eine Daydream ausgeliehen. Die Entwicklung fand allerdings mit einem zweiten Android-Gerät als Controller Emulator statt.


## Umsetzung

Ein Großteil des Settings, nämlich die Becher, die Tischtennisplatte und der Hintergrund, stammen aus dem Unity Asset Store bzw. waren frei im Internet verfügbar.

Zum Anfang des Spiels wird das Spielfeld initialisiert bzw. zurückgesetzt, d.h. die Game Objects der Becher werden entfernt und die Punkte werden auf 0 gesetzt. Daraufhin werden dann Instanzen der Becher-Prefabs auf das Spielfeld gepackt. Außerdem wird über einen Text, der ungefähr im Blickfeld liegt, angezeigt, dass der Spieler selbst an der Reihe ist.


    void startGame() {
            menu.SetActive(false);
            ui.text = "It's your turn. Press button to pick up ball & release it to throw.";
    
            pointsPlayer = 0;
            pointsEnemy = 0;
    
            Destroy(GameObject.Find("cups_red"));
            Destroy(GameObject.Find("cups_blue"));
    
            Object cupObject_red = Instantiate(cups_red, new Vector3(-0.15f, 0.2f, 1.0f), Quaternion.identity);
            Object cupObject_blue = Instantiate(cups_blue, new Vector3(0.15f, 0.2f, -1.0f), Quaternion.Euler(Vector3.up * 180));
            cupObject_blue.name = "cups_blue";
            cupObject_red.name = "cups_red";
    }

Über die Daydream SDK wird die Methode `GvrController.ClickButton` zur Verfügung gestellt. Mit dieser kann der Button des Controllers abgefragt werden. Solange der Button gedrückt gehalten wird, bleibt der Ball in der Hand des Spielers.


    if (GvrController.ClickButton) {
          if (!(isThrown)) {
            PickUp();
            pickedUp = true;
          }
    }


Zusätzlich haben wir hier noch einen boolean `isThrown` abgefragt, der verhindert, dass der Ball direkt nach dem Werfen wieder aufgehoben werden kann.
Bei Loslassen der Taste wird der Ball geworfen, wenn er vorher aufgehoben wurde (`pickedUp`). Dabei wird auch ein Timer gestartet. Nach einer Sekunde zeigt der UI-Text an, dass der Gegner an der Reihe ist. Nach fünf Sekunden wird der Wurf des Gegners simuliert. Solange ist die Steuerung gesperrt.


    if (!GvrController.ClickButton && pickedUp){
          ThrowBall();
          time = Time.time;
          isThrown = true;
          pickedUp = false;
    }


Beim Wurf des Gegners wird zunächst ein Zufallswert ermittelt. Mit einer 40%-igen Wahrscheinlichkeit wird überhaupt ein Becher getroffen.


    float p = Random.value;
    
    if (p < 0.4) {
          DestroyCup();
    }


Die Methode `destroyCup()` wiederum wählt einen zufälligen Becher aus, der getroffen wird. Falls dieser Becher noch im Spiel ist (also das GameObject noch existiert), wird er aus dem Spiel genommen und der Punktestand vom Gegner um 1 erhöht. Zusätzlich haben wir noch ein Wackeln der Kamera eingebaut, das stärker wird je öfter der Gegner trifft.


    void DestroyCup() {
          int cupNumber = Random.Range (0, 10);
          GameObject cupPlayer = GameObject.Find ("cup_blue_" + cupNumber);
    
          if (cupPlayer != null) {
                Destroy (cupPlayer);
                pointsEnemy += 1;
           }
    }


Ob der eigene Wurf erfolgreich war, wird über einen Collider in den Bechern abgefragt. In jedem gegnerischen Becher befindet sich ein kleiner unsichtbarer Würfel. Sobald dieser mit dem Ball kollidiert, wird der entsprechende Becher aus dem Spiel entfernt.


    void OnTriggerEnter(Collider collider) {
            GameObject colliderGameObject = collider.gameObject;
            GameObject cup = colliderGameObject.transform.parent.gameObject;
    
            Destroy (cup);
            Destroy (collider);
    
            pointsPlayer += 1;
    }


Sobald einer der Spieler 10 Punkte erreicht, ist das Spiel vorbei. Hierzu wird ein extra Canvas eingeblendet und bei Klick auf den Button das Spiel zurückgesetzt und neugestartet.


## Was lief gut?

Da wir im letzten Semester schon ein ähnliches Projekt für die Google Cardboard gemacht haben, hatten wir einen gut abgestimmten Workflow. Den größten Teil haben wir in Pair Programming erarbeitet, was bei einem kleinen Projekt wie diesem gut funktioniert, da jeder den gleichen Überblick über den Code hat. Das ist auch der Grund dafür, warum fast alle Git-Commits vom gleichen Account kommen.
Da wir durch das Cardboard schon wussten, dass es meistens nicht auf Anhieb klappt, das VR-Gerät zum Laufen zu bekommen, haben wir bereits am Anfang entsprechend viel Zeit dafür eingeplant, die SDK zu installieren und die Daydream bzw. den Emulator aufzusetzen. Dass durch den Emulator keine Daydream-Brille zur Entwicklung nötig ist, war eine weitere große Erleichterung.


## Was lief nicht gut?

Vor allem dadurch, dass wir im vorherigen Semester bereits ein sehr ähnliches Spiel gemacht haben, war es gegen Ende dieses Projektes die Motivation aufrecht zu erhalten. Der größte Teil wurde bereits relativ früh fertiggestellt. 
Zudem war es relativ schwierig, Bugs zu fixen, weil auf dem mobilen Endgerät anders als im Unity Editor keine direkte Konsole zur Verfügung steht. Dadurch mussten wir noch mehr Zeit als üblich dafür verwenden, Fehler ausfindig zu machen und zu beheben.


## Unterschied geplantes Produkt ↔ Endprodukt

Im Großen und Ganzen wurden alle Kernideen für das Spiel auch implementiert. Lediglich das Skript zum Wackeln der Kamera haben wir nicht selber geschrieben (Quelle: https://gist.github.com/ftvs/5822103). 2D-Texturen (z.B. für die Becher und den Hintergrund) und ein passenderes Setting wären noch schön gewesen, wurden allerdings zugunsten der technischen Umsetzung vernachlässigt. Auch wäre in Zukunft ein Multiplayer denkbar anstatt gegen einen KI-Gegner spielen zu lassen.

