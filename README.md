<p align="center"><img src="src/components/nav/navlogo.png" height="300" width="auto" /></p>

# Pry-Vata

A healthcare privacy allegation tracking mechanism providing CRUD functionality for its users. What sets this application apart from other B2B SaaS healthcare privacy product is that it incorporates a data breach risk assessment, which allows the user to determine if the confirmed allegation is high risk, low risk, or meets an exception. Depending on the risk determined, the investigator may be required to report it to their respective state's Health and Human Services Agency and/or the Office of Civil Rights. This application can allow almost anyone, with basic knowledge of HIPAA, to review and determine the risk of their incident.   

### Built With 
* ReactJS
* CSS
* HTML
* Adobe Photoshop
* C#
* .Net
* SqlServer
* Firebase


## Getting Started

To get a local copy up and running follow these simple steps.

## Requirements

* VSCode
* Visual Studios
* SqlServer
* Git
* NodeJS

## Installation 

* Install NodeJs in your terminal

    `npm install`

* Clone the repository 

    `git clone git@github.com:sxiong0519/PryVata.git`

* In order to create authenticated accounts, create a Firebase account and project
-See directions [here](https://github.com/nashville-software-school/bangazon-inc/blob/main/book-3-web-api/chapters/FIREBASE_AUTH.md) to create a Firebase project and users.

* Once you've created your Firebase project:

 1. Update the appsettings.json file to add a "FirebaseProjectId" key in PryVata.sln in Visual Studios. Make the value of this key your Firebase project id.

 `{
  "FirebaseProjectId": "FIREBASE_PROJECT_ID_HERE"
 }`

 2. Add the <b>Microsoft.AspNetCore.Authentication.JwtBearer Nuget</b> package to your project.

 3. Update the ConfigureServices() method in Startup.cs to configure your web API to understand and validate Firebase JWT authentication.

* 

* Serve the database api
 
 `json-server -p 8088 -w database.json`


* Run the code

    `npm start`

<img src= "Images/HomeExample.gif" height="500" width="auto" />


## Design and Development

### DbDiagram.io - Entity Relationship Diagram

<img src= "Images/ERD.png" height="100" width="auto" />


### WireFrame
<img src= "Images/Homepage.png" height="150" width="auto" />
<img src= "Images/ChildDetail.png" height="150" width="auto" />
<img src= "Images/MileProv.png" height="150" width="auto" />
<img src= "Images/ForLoc.png" height="150" width="auto" />