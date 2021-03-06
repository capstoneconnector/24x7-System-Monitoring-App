# Deployment Documentation

Server Needed: A Postgres Server with pgAdmin 4 is required to store and hold information (https://www.postgresql.org/)

Folder Structure: If cloning from repo or being provided software, no personal edits will be needed or folder rearrangement required

Start and Stop Software: At its current state, the software can be started and stopped within Visual Studio. Once the project is downloaded, as well as the server, you should be able to hit IIS Express Button as seen below: 

![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/unnamed%20(5).png)

When the webpage launches, you can also close that page to stop the project from running. 

Troubleshooting: If problems are encountered, ensure that the repository is installed correctly and that all connections to the database (user and password fields) are filled out correctly. If errors occur while running(no webpage launch), restart visual studio and try again. If the issue persists pull from the repository again and start fresh.

When dealing with Database issues, such as there is a null value, or if there is a value that is incorrectly saved. Open Pgadmin and open up the ReoccurringJob database. As seen below:

![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/pgAdminImage.PNG)

Make sure that all the information is stored correctly and if there are errors you can edit them within Pgadmin.

How to Determine Source of Errors: Within Visual Studio, utilize debugging capabilities in order to find the file(s) that may be causing difficulties and assess from there. Along with this, if there are errors while the program is running, watch from the output source of Visual Studio and see what errors are being thrown.

Most Critical Pieces: Server Connection, Database Connection, Front End CSS, API connection/endpoint. With any of these issues, you can find where the error is stemming from in Visual Studios built in error message page. 
