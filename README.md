# TaskyJ

A proof-of-concept multiplatform application. A simple C# to-do list app that can be run in multiple client/server environments.

## Architecture

### Interface
TaskyJ.Full.sln: solution file containing all aplications/projects

#### Android
TaskyJ.Android.sln: solution file containing all Android-related projects

* `TaskyJ.Interface.Android`: Android native app (version 7.1), using Xamarin

#### SPA
TaskyJ.SPA.sln: solution file containing all SPA-related projects

* `TaskyJ.Interface.Angular`: Angular app
* `TaskyJ.Interface.React`: React app, unfinished

#### Web/ASP
TaskyJ.ASP.sln: solution file containing all ASP-related projects

* `TaskyJ.Interface.AspNetCore`: ASP.Netcore application. Using DotNET 5.0/MVC

#### Windows
TaskyJ.Windows.sln:  solution file containing all Windows-related projects

* `TaskyJ.Interface.Windows`: Windows forms classic application. Using DotNET Framework 4.8
* `TaskyJ.Interface.WPF`: Windows WPF application. Using DotNET Framework 4.8/MVVM
* `TaskyJ.Interface.WPFNetCore`: Windows WPF/netcore application. Using DotNET Framework 5.0/MVVM/WPF Toolkit

#### Database server

* `TaskyJ.STSDB.Server`: Custom NoSQL database, based on https://github.com/STSSoft/STSdb4 , adapted/refactored to Dotnet 5.0
* `TaskyJ.MongoDBServer`: MongoDB launcher, binaries should be located at the "mongoDBbin" folder

#### Database repositories
Several database client implementations exist for several databases

* `TaskyJ.MongoDBServer`: repository implementation for MongoDB
* `TaskyJ.DataRepo.SQLite.Android`: repository implementation for SQLite in Android platform
* `TaskyJ.DataRepo.SQLite.Windows`: repository implementation for SQLite in Windows platform 
* `TaskyJ.DataRepo.SqlSrv.Windows`: repository implementation for SQL Server
* `TaskyJ.DataRepo.STSDB`: repository implementation for STSDB
## License

Released under the [MIT](License) license.
