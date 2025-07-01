# Analisi della Rifattorizzazione di Pacg

In questa pagina raccolgo le note più approfondite sul vecchio progetto **Pacg** mentre lo porto all'interno della nuova code base `NewPacg`. Il documento è il punto di partenza per tutto il resto dell'analisi. Per una panoramica ad alto livello faccio riferimento alla [pagina principale](../PACG_ANALYSIS.md).

## Struttura dei progetti in `Pacg`

* **PgadCommunication** – libreria gateway con DTO di richiesta/risposta e encoder personalizzati.
* **PGADService** – applicazione ASP.NET che ospita endpoint SOAP e utilizza Autofac per l'iniezione delle dipendenze.
* **PgadCommon** – insieme di enumerazioni e costanti condivise.
* **PgadData** – oggetti di accesso ai dati e helper per la connessione.
* **PgadManager** – primo layer di business con metodi ancora in parte abbozzati.
* **PGADServiceLibrary** – classi di utilità usate dall'host del servizio.
* **ClrPgad / ClrPGAD_sql2012** – progetti SQL CLR.
* **Lib** – librerie di terze parti e vecchie dipendenze packages.config.
* **Docs** – documenti di architettura rilasciati dal team originario.

Tutti i progetti sono compilati per **.NET Framework 4.7.2** e fanno ampio uso di WCF. Il processo di build si basa su file *.csproj* in stile classico con riferimenti storici.

## Moduli principali

### PgadCommunication
- La cartella `Business` espone i concetti di dominio, ad esempio `Bonus`.
- In `Configuration` gestisco gli endpoint di servizio e i certificati X509.
- Il sottoalbero `Contracts` contiene centinaia di classi DTO sotto `Requests` e `Responses`.
- `CustomMessageEncoder` implementa encoder SOAP specifici per i protocolli AAMS.
- In `Factory.cs` creo le istanze del gateway in base alla configurazione.

### PGADService
- Rappresenta l'host WCF (`PGADService.svc` e `.json.svc`).
- In `Global.asax` configuro l'iniezione delle dipendenze e le route dei servizi.
- I moduli Autofac registrano `PgadManager` e i servizi del livello dati.

### PgadData
- Livello di accesso ai dati con classi DAO come `PacgProtocolloDao` e `TransazioniDao`.
- La cartella `Entity` fornisce semplici POCO (`HeaderInfo` ecc.) utilizzati dai DAO.

### PgadManager
- È un wrapper minimale sopra `PgadData` che espone i punti di ingresso della logica di business.
- Nel codice legacy molti metodi sono ancora TODO o semplici pass-through.

## Considerazioni per il refactoring

Il nuovo `NewPacg` segue la struttura di riferimento di **grattaevinciapi**:

```
NewPacg.sln
├── NewPacg           (ASP.NET Core Web API)
├── NewPacg.Common    (contratti, entità, enum)
├── NewPacg.Data      (astrazioni per l'accesso ai dati)
├── NewPacg.Manager   (servizi di business)
└── NewPacg.Tests     (test xUnit)
```

Trasferirò progressivamente le funzionalità da `PgadCommunication`, `PGADService` e dagli altri progetti all'interno di questo nuovo layout. I primi passi riguardano la modernizzazione dei DTO e dei contratti di servizio, seguiti dal livello dati e dalla logica di business.

* Utilizzo **.NET 6** con progetti in stile SDK.
* Sostituisco gli endpoint WCF con controller ASP.NET Core Web API.
* Introduco l'iniezione delle dipendenze tramite `Microsoft.Extensions.DependencyInjection`.
* Abbandono packages.config a favore dei riferimenti tra progetti SDK.
* Aggiungo test unitari completi in `NewPacg.Tests` man mano che migro i componenti.

Man mano che la migrazione procede, documenterò in pagine dedicate ogni componente legacy, collegate a partire da questo file.
