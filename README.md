# ShadyBookAppV2


Då vi antar att du redan har SQL server installerat och fungerande så hoppar vi över det steget.  

Gå till https://github.com/patrik033/ShadyBookAppV2 och ladda ner sista varianten 

Se till att du är kopplad till “(localdb)\MSSQLLocalDB” som server 

Kontrollera att Connectionsträngen (i appsettings  

Server=(localdb)\\MSSQLLocalDB;Database=ShadyBookApp;Trusted_Connection=True;) 

Du borde få med alla nuggetpaketen som ska behövas men ifall det inte så skulle vara fallet så skriver jag en lista på dem du behöver 

Microsoft.EntityFrameworkCore (6.0.1) 

Microsoft.EntityFrameworkCore.Design(6.0.1) 

Microsoft.EntityFrameworkCore.sqlServer (6.0.1) 

Microsoft.EntityFrameworkCore.Tools(6.0.1) 

Microsoft.Extensions.Configuration (6.0.1) 

Microsoft.Extensions.Configuration.Json (6.0.1) 

Alla dessa kan installeras via Package Manager Console (Om den inte finns i botten av VS så gå till Tools=> NuGet Package Manager => Package Manager Console 

Skriv in “Install-Package ” (mellanslag) => Följt av paketet som ska installeras (Paranteserna med version behövs inte) 

När du kan se alla nuggetpaketen ovan inne i Dependencies=>Packages så kan du gå ner igen och skriva in Update-database (Migration behövs inte då dessa följer med programmet) 

Om inget felmeddelande har uppstått så kan du nu köra programmet. Börja med att välja “StartUp” i menyn för att ladda in testdata!  

KLAR! 

OBS denna guide är för att skapa en NY databas, det går inte att använda den ifall databasnamnet redan existerat!  

 

 

 
