# NewPacg

Questa directory raccoglie la nuova soluzione che sto utilizzando per rifattorizzare il progetto legacy **Pacg**. Mi ispiro alla struttura a livelli del progetto di riferimento `grattaevinciapi` e punto a .NET 6 per tutte le librerie.

## Progetti

- `NewPacg` – è l’ASP.NET Core Web API della soluzione. Al momento non ho ancora implementato endpoint, ma qui troveranno posto quelli nuovi man mano che porterò le funzionalità della vecchia applicazione.
- `NewPacg.Manager` – questo progetto sarà dedicato ai servizi di business. Per adesso contiene soltanto lo scheletro delle classi che ospiteranno la logica applicativa.
- `NewPacg.Data` – definisce solo le astrazioni per l’accesso ai dati, come le interfacce dei repository.
- `NewPacg.Common` – include contratti condivisi, entità e enumerazioni utili a tutti i livelli dell’applicazione.
- `NewPacg.Tests` – la suite di test xUnit che mi aiuta a convalidare ogni componente durante lo sviluppo.

Tutti i progetti sono inclusi nel file di soluzione `NewPacg.sln`, che mi permette di gestirli comodamente sia in Visual Studio sia tramite la CLI di dotnet. Al momento sono presenti solo le astrazioni di base. La logica di business effettiva verrà sviluppata progressivamente durante la migrazione dal vecchio progetto Pacg e troverà posto nei moduli indicati.
