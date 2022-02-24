# EF Core 6 Demo
Demo voor EF Core 6 Tech Thursday

# Meer lezen?
## Entity Framework Core 6 (EF Core 6)
https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/whatsnew

* **Temporal tabellen**: toevoegen van historie aan een tabel. Validatiedatums voor de data. Hier kunnen ook queries op gemaakt worden
* **Migration Bundles**: Dit lijkt redelijk op DbUp (straks meer) waarbij je migratie kan doen met een executable die in een CI/CD omgeving geïmplementeerd kan worden
* **Pre-convention model configuratie**: hiermee kan standaard configuratie opgegeven worden voor data types. Bv een string altijd maximaal 50 karakters land
* **Compiled modellen**: met het optimize commando zorgt voor een snellere startup bij grote modellen (veel types en relaties)
* **Betere LINQ queries**: betere ondersteuning voor group by.
* **UnicodeAttribute**: op de entity kan je aangeven of er wel of niet unicode gebruikt mag worden.
* **Model building verbeteringen**: sparse kolommen (kolommen die vaak null zijn), conversie ondersteuning, many-to-many makkelijker

## Entity Framework Core 7 (EF7)
https://devblogs.microsoft.com/dotnet/announcing-the-plan-for-ef7/

**JSON Kolommen**: JSON data in een kolom die vervolgens doorzocht kan worden.
**Bulk updates**: updaten van regels in de database zonder entities eerst in geheugen te laden
**Grafische User Interface**: Betere ondersteuning voor Windows Forms en .NET MAUI.
**Migratie van EF6 naar EF7**: Ontbrekende features uit EF6 (niet te verwarren met EF Core 6) opnemen in EF7
**Performance**: Net als altijd wordt er gestreefd naar betere performance

