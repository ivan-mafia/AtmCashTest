# AtmCashTest
Test solution for the interview.

Description of the AtmCashTest solution:

-AtmCashTest.WcfService - WCF service (IIS hosting) for simulating ATM operation.
-AtmCashTest.WpfClient - WPF client using WCF service to simulate ATM operation.

-AtmCashTest.WebApi - Asp.Net WebApi / AngularJS web application simulating the work of the ATM.

-AtmCashTest.Core - Basic models, the logic of issuing bills.
-AtmCashTest.Data - Data service (EF 6 Code-First).

Assembly / use instructions:
Build with Visual Studio 2015 Update 3:
1. Open the AtmCashTest.sln solution
2. Make sure that all nuget packages are recovered / loaded.
3. Make sure that you have an SQL server.
3. Run the Debug configuration of the AtmCashTest solution. (Screenshot1.PNG)
4. After that WCF service with WPF client should start, then WebApi will start. (Screenshot2.PNG)
5. The application logs are located in:
. \ AtmCashTest.WcfService \ WCFLog \ {date} -atm-wcfservice.log
. \ AtmCashTest.WebApi \ APILog \ {date} -atm-webapi.log
. \ {Output or WPF client * .exe dir} \ WPFclientLog \ {date} -atm-wpfclient.log

List of additional libraries used:
- Entity Framework 6.1.3 - ORM
- Ninject 3.2.0 - DI container
- NLog 4.4.1 - logger
- Catel 4.5.4 - MVVM Framework (WPF)
- AutoMapper 5.2.0 - object mapper
