# MoneyFlow

MoneyFlow is a web app written in .NET CORE 3.1 to track your money flow

## Tools needed
1. Docker engine
2. dotnet sdk 3.1.426
3. Internet browser

## How to use
1. Clone this repository<br />
  `git clone git@github.com:rnr3007/MoneyFlow.git`
2. Run the SQL Server<br />
  `docker compose -f sql-server.yml up -d`
3. Run the dotnet application<br />
  `dotnet run`
4. Open the internet browser and go to `localhost:5000` as its default base URL 
