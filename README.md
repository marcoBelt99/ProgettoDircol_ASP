# ProgettoDircol_ASP
Progetto per il tirocinio formativo da 12 CFU, Unife - Realizzazione di un sito web Aziendale in ASP.NET Web Forms

## Specifiche, Rappresentazione concettuale e logica del progetto: 
[Schema Database](https://app.diagrams.net/?mode=google&gfw=1#G1CpOvL8520L8bq4log0TpyE_K_N3e4UsV)

## Elenco dei problemi che possono capitare
* Per problemi legati alla compilazione del progetto (con il compilatore Roslyn):
![image](https://user-images.githubusercontent.com/74368037/147921679-688bb88c-c753-4b98-be14-e4a94448c819.png),
provare a dare questo comando nella Console di gestione dei pacchetti di Visual Studio:

`Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r`

* Altri problemi ...

## Struttura del progetto
Il progetto è costituito da alcune pagine che rappresentano le operazioni di DML (Data Manipulation Language) delle principali entità (capo, modello, puntovendita, dipendente, vendita). Gli altri file :

#### Dump del Database
Anzitutto, il [File .sql del database:](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/dircol_SQLSERVER.sql), che contiene il codice per il 'dump' in ambiente SQL Server.

#### Stringa di connessione
* Nel file [**Web.config**](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/Web.config) è presente la **stringa di connessione**, necessaria per il collegamento al Database

#### Pagine Web
* Home page: [Default.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Default.aspx)
* Contatti: [contatti.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/contatti.aspx)
* Chi siamo: [chiSiamo.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/chiSiamo.aspx)
* Pagina per gli inserimenti: [inserisci.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/inserisci.aspx)
* Pagina per gli aggiornamenti: [aggiorna.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/aggiorna.aspx)
* Pagina per le eliminazioni: [elimina.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/aggiorna.aspx)
* Pagina per la gestione dei modelli: [gestioneModelli.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/gestioneModelli.aspx): serve agli utenti registrati per poter visualizzare i modelli attualmente disponibili che classificano almeno un capo d'abbigliamento.
* Pagina per i dettagli di un singolo modello: [dettaglioModello](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/dettaglioModello.aspx): serve agli utenti registrati per visualizzare le informazioni specifiche di un modello e per visualizzare la lista dei capi classificati da tale modello.
* Per le pagine di: *registrazione* e *login* di un utente (ordinario), e di *login* di un utente amministratore, andare alla sezione 'Gestione ruoli utenti', qui in basso. 
* Pagina  [visualizzaTabelle.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/visualizzaTabelle.aspx) in cui è presente un elemento bootstrap "*accordion*" che servirà all'amministratore del sito per visualizzare lo schema della base di dati.

#### Classi implementate
Collegati ad essi, all'interno del direttorio: [Dati](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati) si trovano tutte le classi delle relative *entità* di base succitate, tutte contenenti "operazioni di CRUD" (per ora solo inserimenti, successivamenti anche modifiche e cancellazioni. Per le modifiche e le cancellazioni si faccia riferimento al [Progetto dirocol in PHP](https://github.com/marcoBelt99/ProgettoDircol_PHP)):
* [Capo.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Capo.cs), 
* [Modello.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Modello.cs), 
* [PuntoVendita.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/PuntoVendita.cs),
* [Dipendente.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Dipendente.cs), 
* [Transazione.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Transazione.cs).
* [Utente.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Utente.cs).
* [Amministratore](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Amministratore.cs)
Inoltre, la classe [Operazione.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Operazione.cs) è di notevole utilità, poichè offre molte operazioni con le classi delle entità, che permettono di: estrarre alcune liste di elementi inseriti in alcune tabelle del database, contiene la stringa di connessione al DB, ed altri metodi per effettuare operaazioni utili.

Si noti che, ogni pagina \*.aspx  è accompagnata dalla relativa pagina contente il **'Code Behind'**, ossia la '*business logic*' della pagina, avente estensione \*.aspx.cs

##### Gestione dei Ruoli Utente
Di notevole importanza è il file: [Site.master](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/Site.master), il quale contiene il layout di tutto il sito (di tutte le pagine che la ereditano). Nel Code Behind di tale file sto implementando anche la questione dei **Ruoli utente**, per l'appunto:
* Nel file [registrazioneUtente.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/registrazioneUtente.aspx) è presente, di fatto, la query di inserimento di un nuovo utente (ordinario) nella tabella '*utenti*' del database. In tale file, la selezione di uno stato dalla drop down list,è stata una "scusa" per poter vedere ed implementare un'operazione di lettura dal [file](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/statiMembri.txt) in c#.
* Nei file [loginUtente.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/loginUtente.aspx) è presente la query di ricerca di un utente presente nel database attraverso il proprio username e password (ciò implica che sto cercando un utente già registrato)
* Nell'analogo file [loginAdmin](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/loginAdmin.aspx) è presente la medesima query di ricerca di un amministratore nella tabella *amministratori* del database.
In pratica, vi saranno **3 tipi di utenti**: utente non registrato, utente ordinario registrato ed utente amministratore:
1. Utente non registrato: non ha accesso ad alcune pagine web, ed ad alcuni link, es. il *log-out*.
2. Utente ordinario registrato: ha accesso a tutte le pagine web, tranne quelle riservate all'amministratore.
3. Amministratore: ha accesso a tutte le pagine, tranne quella di registrazione di un utente e di gestione modelli (non può acquistare).
In [*Site.master.cs*](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/Site.Master.cs) sto utilizzando la variabile **Session[]** per poter gestire i diversi ruoli utenti. Session è inizializzata nei due file di login degli utenti (ordinari ed amministratori). Mi sono assicurato che non risulti*null* (ossia che venga inizializzata correttamente). Ho bypassato questo problema inserendo alcune istruzioni di controllo nel *Site.Master.cs*. Di seguito riporto una tabella riepilogativa sull'accesso consentito e non consentito delle varie pagine del sito.

| Pagina\utente     | Utente non loggato | Utente loggato | Amministratore |
| ---| --- | --- | --- |
| *interrogazioni.aspx* | :x: | :x: | :heavy_check_mark: |
| *inserisci.aspx* | :x:  | :x: | :heavy_check_mark: |
| *aggiorna.aspx*  | :x: | :x: | :heavy_check_mark:|
| *elimina.aspx*   | :x: | :x: | :heavy_check_mark: |
| *visualizzaTabelle.aspx* | :x: | :x: | :heavy_check_mark: |
| *loginAdmin.aspx* | :heavy_check_mark: | :heavy_check_mark: | :x: |
| *loginUtente.aspx* | :heavy_check_mark: | :x: | :x: |
| *registrazioneUtente.aspx* | :heavy_check_mark: | :x: | :x: |
| *gestioneModelli.aspx* | :x: | :heavy_check_mark: | :x: |
| *dettaglioModello.aspx* | :x: | :heavy_check_mark: | :x: |
| *visualizzaCarrello.aspx* | :x: | :heavy_check_mark: | :x: |
| *pagamento.aspx* | :x: | :heavy_check_mark: | :x: |


#### Gestione del carrello acquisti:
E' stato implementato utilizzando alcuni file ed alcune strutture dati. I file li ho usati *come se fossero delle tabelle*. Partiamo dai **file:**
* [CapiAggiuntiAlCarrello](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/CapiAggiuntiAlCarrello.txt) contiene l'elenco di tutti i capi aggiunti al carrello. E' come se fosse un "doppione" della tabella 'capi' del Database.
* [Carrelli.json](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/Carrelli.json) contiene un elenco di oggetti di tipo 'Carrello'.
Le **strutture dati** utilizzate sono: alcune Liste (List<TipoOggetto>) e Dizionario. Sostanzialmente, si prelevano i dati dal file .txt (lettura) e si manipolano usando operazioni di join tra le due. Si fanno operazioni di ricerca, inserimento, eliminazione in lista, anche usando **LINQ**.
Una volta che si è fatta l'operazione, che si è modificata strutturalmente la lista, potremo andare a riscriverla nei rispettivi file.
ListaCarrelli, DizionarioCapiAggiuntiAlCarrello... .......................... (mettere immagine su draw.io)  
