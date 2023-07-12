# HackerNewsApi

Santander - Developer Coding Test

How to run:
1) cd .\HackerNewsApi.Api\ 
2) dotnet run
If you get https related error, run:
3) dotnet dev-certs https --trust

Assumptions:
1) Auth is not required.
2) List of story ids will change more often than particular story. 
I assume that ranking might be updated quite frequently (people vote etc) while story itself not so often.
This is the reason I use different cache retention times for those different objects.

Things to improve given a time:
1) Better exception handling.
2) Better test coverage. At the moment only service layer is tested. 
3) Consider using distributed cache if application needs to scale horizontally.
4) Optimize cache retention time values.
5) Introduce http resilience framework e.g Polly. It's strongly recommended as we don't own HackerNewsApi and must be prepared for its failure anytime.