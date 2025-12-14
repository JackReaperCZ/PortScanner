# Paralelní LAN Scanner

- Autor: Dominik Pour
- Kontakt: domcapour1@gmail.com
- Škola: SPŠE Ječná
- Datum vypracování: 2025‑11‑24
- Projektový status: školní projekt
- Verze: Initial.

## Požadavky a Use Case

- Business requirements:
  - Uživatel chce skenovat velké rozsahy IPv4 adres bez zamrzání UI.
  - Během skenu chce vidět průběžné výsledky a log.
  - Chce filtrovat výsledky, řadit je a exportovat do CSV/JSON/XML.
- Chce konfigurovat počet workerů, DNS lookup.
- Chce v detailu hosta skenovat služby (HTTP/HTTPS/FTP) v zadaném rozsahu portů a vidět průběžný log.
- Functional requirements:
  - Zadání `Start IP` a `End IP` v GUI.
  - Nastavení `Workery`.
  - Producent–konsument architektura s `BlockingCollection<IPAddress>`.
- `Ping`, volitelný `DNS lookup`.
  - Tabulka s průběžnými výsledky, numerické řazení IP a RTT.
  - Živý log s časem a ID workeru.
  - Export do CSV/JSON/XML s aplikovanými filtry, blokace exportu při prázdné tabulce.
  - UI aktualizace přes `Invoke()/BeginInvoke()` a batchování přes `Timer`.
- Use Case :
  - UC‑1 Spustit sken: Uživatel zadá rozsah a parametry, klikne `Start`, průběžně sleduje výsledky.
  - UC‑2 Zastavit sken: Klikne `Stop`, běh se přeruší, UI zůstane stabilní.
  - UC‑3 Filtrovat: Uživatel nastaví filtry.
  - UC‑4 Exportovat: Uživatel exportuje aktuálně zobrazené výsledky do CSV/JSON/XML.
  - UC‑5 Řadit: Klik na hlavičku sloupce řadí IP numericky, RTT číselně.

## Architektura

- Složky:
- `Models/ScanRecord.cs` – datový model výsledku.
- `Services/Scanner.cs` – producent–konsument skener, eventy `Record`, `Log`, `Completed`.
- `Utils/IpUtils.cs` – pomocné IP utility: `TryParseIPv4`, `FromUInt32`, `IpToUInt32`, `ParsePorts`.
- `MainForm.cs`, `MainForm.Designer.cs` – hlavní UI, filtry, řazení, export, virtual list, konzole.
- `HostDetailsForm.cs`, `HostDetailsForm.Designer.cs` – detail hosta, skenování služeb v rozsahu portů, filtr stavů a konzole.
- Vzory:
  - Producer–Consumer: `BlockingCollection` + `Task.Run`.
  - Observer: eventy `Record`, `Log`, `Completed` v `Scanner`.
  - UI Virtualization: `ListView.VirtualMode` a `RetrieveVirtualItem`.
  - Cancellation: `CancellationTokenSource` v `Scanner.Stop()`.
  ---

  ![image](./diagram.png)

  ---
## Behavior

- Activity flow:
  1. Start → Validace IP → Nastavení workerů → Spuštění Scanner → Generování IP → Konzumenti z fronty.
  2. Pro IP: Ping → (DNS) → Emitovat `Record` + `Log`.
  3. Hlavní formulář batchuje eventy (Timer) → aktualizuje `filteredResults` → invaliduje `VirtualListSize`.
  4. Stop → `CancellationTokenSource.Cancel()` → `Completed` → UI Timer stop.
  5. Detail hosta: po dvojkliku na záznam se otevře `HostDetailsForm`, kde lze skenovat služby v rozsahu portů; průběžné výsledky se zapisují do tabulky a logu.

## Rozhraní, protokoly a závislosti

- Rozhraní/protokoly:
  - ICMP Ping (`System.Net.NetworkInformation.Ping`).
  - DNS (`System.Net.Dns.GetHostEntryAsync`).
  - TCP connect (`System.Net.Sockets.TcpClient`).
- Závislosti:
  - .NET Framework 4.8, Windows Forms.
  - Testy: NUnit, NUnit3TestAdapter, Microsoft.NET.Test.Sdk.
  - Distribuce: Fody + Costura.Fody (vložené závislosti do EXE).
- Nefunkční požadavky:
  - Stabilita UI, nízká paměťová náročnost (virtual list, lazy generation, bounded queue).
  - Paralelismus řízený `SemaphoreSlim`.

## Konfigurace programu

- Hlavní formulář:
  - `Start IP`, `End IP` – rozsah IPv4 (vstup validován; `start ≤ end`).
  - `Workery` – počet souběžných konzumentů (≥ 1).
  - `DNS lookup` – zapnout/vypnout.
  - Filtry: `Stav`, `IP filtr`, `Hostname filtr`, `RTT min/max`.
- Detail hosta:
  - `HTTP`, `HTTPS`, `FTP` – volba služeb.
  - `Porty` – rozsah `start–end` (1–65535; `start ≤ end`).
  - Filtr stavů: `All`, `Online`, `Offline`, `Error`.
  - Konzole s průběžnými logy workerů.

## Instalace a spuštění

- Požadavky: .NET Framework 4.8, Windows.
- Build: `dotnet build` v kořeni projektu.
- Spuštění: `bin\Release\net48\PortScanner.exe`.
- Testy: `dotnet test`.

## Chybové stavy a řešení

- Neplatný rozsah IP: zobrazí se log „Neplatný rozsah“, sken se neprovede.
- Timeout ping: stav `Timeout`, bez RTT.
- DNS výjimky: zachyceny, hostname prázdný.
- Detail hosta – timeout HTTP/FTP: stav `Timeout`, případně `Offline`.
- Export při prázdné tabulce: blokováno, informativní message box.
- UI interakce během skenu: batchování přes Timer, global exception handler (`Program.cs`) zachytává UI chyby.

## Ověření, testování a validace

- Jednotkové testy (`Tests/PortScanner.Tests`):
  - `TryParseIPv4`: valid, invalid, nula a broadcast, odmítnutí IPv6.
  - `IpToUInt32` + `FromUInt32`: round‑trip a hraniční hodnoty.
  - `ParsePorts`: filtr platného rozsahu, zachování duplicit.
- Výsledek posledního běhu: všechny testy úspěšné.
- Validace požadavků: aplikace splňuje funkční požadavky a nefunkční požadavky na stabilitu UI.

## Síť

- Skenuje IPv4 rozsahy přes ICMP, DNS a TCP connect.
- Doporučení: zajistit přístup k ICMP (firewall), některé prostředí blokují ping.

## SW závislosti

- Závisí na systémových síťových API Windows.

## Import/Export schéma

- CSV:
  - Hlavička: `Ip,Status,RTT,Hostname`.
  - Oddělovač: `,`.
  - Povinné: `Ip`, `Status`. Nepovinné: `RTT`, `Hostname`.
- JSON:
  ```json
  [
    { "ip": "172.0.0.1", "status": "Online", "latency": 35, "hostname": "server.local" },
    { "ip": "172.0.0.2", "status": "Offline", "latency": null, "hostname": null }
  ]
  ```
- XML:
  ```xml
  <ScanResults>
    <Result>
      <IP>172.0.0.1</IP>
      <Status>Online</Status>
      <RTT>35</RTT>
      <Hostname>server.local</Hostname>
    </Result>
    <Result>
      <IP>172.0.0.2</IP>
      <Status>Offline</Status>
    </Result>
  </ScanResults>
  ```

## Odkazy na zdrojový kód

- Spuštění aplikace: `Program.cs:28`.
- Hlavní GUI – filtry a export: `MainForm.Designer.cs` a `MainForm.cs:41–92, 112–211`.
- Virtual List a batch UI: `MainForm.cs:333–468`.
- Dvojklik na záznam → detail hosta: `MainForm.cs:406–418`.
- Detail hosta – průběžné výsledky a log: `HostDetailsForm.cs:94–121, 155–175`.
- Skenovací služba: `Services/Scanner.cs`.
- IP utility: `Utils/IpUtils.cs`.

## License

- MIT License 2025 Dominik Pour.
