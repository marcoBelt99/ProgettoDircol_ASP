# ProgettoDircol_ASP
Progetto per il tirocinio formativo da 12 CFU, Unife - Realizzazione di un sito web Aziendale di mia invenzione in ASP.NET Web Forms

## Rappresentazione concettuale e logica del progetto:
[Schema Database](https://app.diagrams.net/?mode=google&gfw=1#G1CpOvL8520L8bq4log0TpyE_K_N3e4UsV)

## Elenco dei problemi che possono capitare
Per problemi legati alla compilazione del progetto (con il compilatore Roslyn), provare a dare questo comando nella Console di gestione dei pacchetti di Visual Studio:
`Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r`

## Struttura del progetto
Il progetto è costituito da alcuni file che rappresentano le operazioni di DML (Data Manipulation Language) delle principali entità (capo, modello, puntovendita, dipendente, vendita):
* Anzitutto, il [File .sql del database:](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/dircol_SQLSERVER.sql), che contiene il codice per il 'dump' in ambiente SQL Server.
* Home page: [Default.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Default.aspx)
* Pagina per gli inserimenti: [inserisci.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/inserisci.aspx)
* Pagina per gli aggiornamenti: [aggiorna.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/aggiorna.aspx)
* Pagina per le eliminazioni: [elimina.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/aggiorna.aspx)

Collegati ad essi, all'interno del direttorio: [Dati](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati) si trovano tutte le classi delle relative entità di base succitate:
* [Capo.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Capo.cs), 
* [Modello.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Modello.cs), 
* [PuntoVendita.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/PuntoVendita.cs),
* [Dipendente.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Dipendente.cs), 
* [Vendita.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Vendita.cs).

Inoltre, la classe [Operazione.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Operazione.cs) è di notevole utilità, poichè offre molte operazioni con le classi delle entità, che permettono di: estrarre alcune liste di elementi inseriti in alcune tabelle del database, contiene la stringa di connessione al DB, ed altri metodi per effettuare operaazioni utili.

Si noti che, ogni pagina \*.aspx  è accompagnata dalla relativa pagina contente il **'Code Behind'**, ossia la '*business logic*' della pagina, avente estensione \*.aspx.cs

Il file [visualizzaTabelle.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/visualizzaTabelle.aspx) in cui è presente un elemento bootstrap "accordion" che serve all'amministratore del sito per visualizzare lo schema della base di dati.

###### Gestione dei Ruoli Utente
Di notevole importanza è il file: [Site.master](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/Site.master), il quale contiene il layout di tutto il sito (di tutte le pagine che la ereditano). Nel Code Behind di tale file sto implementando anche la questione dei **Ruoli utente**, per l'appunto:
* Nel file [registrazioneUtente.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/registrazioneUtente.aspx) è presente, di fatto, la query di inserimento di un nuovo utente (ordinario) nella tabella '*utenti*' del database. In tale file, la selezione di uno stato dalla drop down list,è stata una "scusa" per poter vedere ed implementare un'operazione di lettura dal [file](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/statiMembri.txt) in c#.
* Nei file [loginUtente.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/loginUtente.aspx) è presente la query di ricerca di un utente presente nel database attraverso il proprio username e password (ciò implica che sto cercando un utente già registrato)
* Nell'analogo file [loginAdmin](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/loginAdmin.aspx) è presente la medesima query di ricerca di un amministratore nella tabella *amministratori* del database.

In [*Site.master.cs*](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/Site.Master.cs) sto utilizzando la variabile **Session[]** per poter gestire i diversi ruoli utenti. Session è inizializzata nei due file di login degli utenti (ordinari ed amministratori). Tuttavia, per il momento tale variabile risulta sempre *null*.
