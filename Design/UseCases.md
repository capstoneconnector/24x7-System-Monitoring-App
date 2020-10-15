#Use Cases

##Actors

-	Accutech Admins - The admins will be the primary users for this product as they will moderate the system and respond to any warnings it may provide
    
-	Potential Third Party - While Accutech may be the primary user and owner of the product, they have mentioned that if the need arises, they may hire an external team to use the software and help respond with any issues that appear

##Use Cases

-	UC1 - General System Health - Overall, the software is meant to generally monitor the health of the products and give a quick glance over view as opposed to looking at each one individually. The actor for this would be the admins most likely and the flow would simply be going to the page (most likely with correct credentials) and looking at the dashboard. This is tied to BR1 (Ensure Product Health/Uptime).
    
-	UC2 - Alerts in Response to Errors - Using UC3 information on errors will be collected. These errors will be viewed on criteria on certain levels of severity. These can lead to different types of alerts to be sent out. Such as emails or texts to the Accutech Admins or Third Party. Will update UC1 with general health checks. This is tied to BR2 (24/7 Monitoring (Especially Night time)).
    
-	UC3 - API calls from other applications - This software should be able to get information from other software. The software will automatically be taking information from these other applications 24x7. It’s going to get the information from API calls. The information that comes in will be checked for any errors which will result in UC2. The admins at Accutech are the actors for this use case who are the ones who need to view the information coming in.This is tied to BR3 (Centralized Command Console)
