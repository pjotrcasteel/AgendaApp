# AgendaApp
Small Web Api + App created of the weekend.
Because I could find any non-production application code to show as review, I have created this app over the weekend instead.
I used a combination of .NET core 3.0 (Web Api - RESTfull) and the latest version of Vue (front-end app)

How use it:
1) Clone
2) Open AgendaApp.sln in VS
  2a) Update-Database
  2b) Run (eg. as IIS Express) - Note: in Program line 22, all data is removed from context on init.
3) Open command 
  3a) Navigate to ~\AgendaApp\AgendaApp.Frontend\agenda
  3b) Run npm install
  3c) Run npm run serve
4) Navigate to http://localhost:8080/
5) Play around.
  
Footnote: app is started as dev per default, web api server url is port: 51044
