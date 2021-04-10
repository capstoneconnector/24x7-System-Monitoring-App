# User Documentation
If a user has installed postgreSQL and pgAdmin they will be able to access pgAdmin with their master password. They then can enter the password for our database (plant123) and they will be able to see the scheduled checks our software can perform, after following the deployment steps below.

## TO DEPLOY:

1) Find clone link for software and and copy this path URL

![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/unnamed.png)

2) Open Visual Studio 2019 and find the solution if already on system or if not, follow step 3) 

![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/unnamed%20(1).png)

3) (Optional) If cloning fresh, click on clone a repository

![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/unnamed%20(2).png)

4) Enter the Repository URL gathered from BitBucket into the Repository location. If there are errors, make sure to have just the location, with no text in front 

![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/unnamed%20(3).png)

5) Project should open up from this step. Make sure that you have Three main folders on the right, SystemMonitoring, SystemMonitoring.Backend and SystemMonitoring.Test

6) At the top, select Tools > Command Line > NugetPackageManager > Package Manager Console >type “Update-Database” and press Enter. Should have a build start update and complete

![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/unnamed%20(4).png)

7) After all of this is done you should be able to run the program. Go to the top and click on IIS Express and it should launch the local webpage

![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/unnamed%20(5).png)

 8) After this runs, if you want to make sure that there is information stored in the database check the webpage in the middle of the screen
 
9) The home page displays all the currently running tasks as well as their status. You also have the option to delete, edit, or run any of the tasks.

![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/HomeScreen.PNG)

10) To add a new task click on ‘Add Task’ in the top navigation bar. In the add task page you can enter the name, url of the api endpoint, cron string for how often you want the job to be repeated, the field you want to check, and the condition the field should meet. You also can create a new group and then add that notification group for when a tasks fails. It will then be able to know what group to notifiy. Then click save new task to add it.

![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/AddTask.PNG)

11) If you want to edit a task to be able to change information on what is being tested or when, you can click the edit button on a task and it will take you to a page with that information already plugged in. 

![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/EditTask.PNG)

12) Another option at the top of the page is an add user page that is used to add contact information for users that would need to be notified within the system. Here you will enter basic info like name, phone number email and what contact group they are in. 

![alt text](https://github.com/Hjwallace/24x7-System-Monitoring-App/blob/master/Auxiliary%20Files/AddUser.PNG)
