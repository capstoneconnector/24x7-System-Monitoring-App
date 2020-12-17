# User Documentation
If a user has installed postgreSQL and pgAdmin they will be able to access pgAdmin with their master password. They then can enter the password for our database (plant123) and they will be able to see the scheduled checks our software can perform, after following the deployment steps below.

## TO DEPLOY:

Find clone link for software and and copy this path URL
![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/unnamed.png)

Open Visual Studio 2019 and find the solution if already on system or if not, follow step 3) 
![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/unnamed%20(1).png)

(Optional) If cloning fresh, click on clone a repository
![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/unnamed%20(2).png)

Enter the Repository URL gathered from BitBucket into the Repository location. If there are errors, make sure to have just the location, with no text in front 
![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/unnamed%20(3).png)

Project should open up from this step. Make sure that you have Three main folders on the right, SystemMonitoring, SystemMonitoring.Backend and SystemMonitoring.Test

At the top, select Tools > Command Line > NugetPackageManager > Package Manager Console >type “Update-Database” and press Enter. Should have a build start update and complete
![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/unnamed%20(4).png)

After all of this is done you should be able to run the program. Go to the top and click on IIS Express and it should launch the local webpage
![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/unnamed%20(5).png)

 After this runs, if you want to make sure that there is information stored in the database check the webpage in the middle of the screen
 
To run tests click on tests at the top of the page and click on run all tests
![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/unnamed%20(6).png)
