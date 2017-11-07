# Deep Space Hair Salon

#### by James Osborn

### Description

This is an application for a Hair Salon, made with C#. The employer will be able to add stylists to the salon, view those stylist individually or as a list, and then assign clients to individual stylists. Furthermore, they can update a stylists name, view clients of a particular stylist, or delete a client from a stylist if the client no longer comes in to the salon.

This application contains only back-end functionality.

### Setup/Installation Requirements

-Download GitHub  
-Clone the GitHub repository at https://github.com/jamescosborn/hair-salon.git  
-Download Mono  
-Navigate to project directory  
-In Mono Command Prompt, type `dotnet restore`  
-In Mono Command Prompt, type `dotnet test`  
-Verify all tests are passing on the back-end

### Database Reconstruction

To recreate this project's database, use the following commands in SQL:

CREATE DATABASE james_osborn_test;  
USE james_osborn_test;  
CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));  
CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylist_id INT);  

### Specs

#### Description: The employee should be able to see a list of stylists.  
Example Input: `User visits page.`  
Example Output: `Tony, Stephanie, Lisa, Melissa`  


#### Description: The employee should be able to add stylists to the system when they are hired.  
Example Input: `Add: Shadowbeast`  
Example Output: `Stylists: Tony, Stephanie, Lisa, Melissa, Shadowbeast `

#### Description: The employee should be able to click on any stylist to see their details.  
Example Input: `User clicks Melissa.`  
Example Output: `Melissa Age: 99 Favorite music: surf  `

#### Description: The employee should be able to see a list of a stylist's clients whenever they click on a stylist.  
Example Input: `User clicks Melissa.`  
Example Output: `Clients: Mon Mothma, Chewbacca, Bodhi Rook`  

#### Description: The employee should be able to add a client to a stylist.  
Example Input: `Add Client Bob to Stylist Lisa.`  
Example Output: `Lisa' Clients: General Grievous, Shawn, Abby, Bob`  

#### Description: The employee should be able to update a client's name.  
Example Input: `Update Bob's name to General Bob.`  
Example Output: `Client name updated! Lisa's Clients: General Grievous, Shawn, Abby, General Bob`  

#### Description: The employee should be able to delete a client if they no longer visit the salon.  
Example Input: `Delete General Grievous`  
Example Output: `Client Deleted! Lisa's Clients: Shawn, Abby, General Bob`


### Known Bugs
No known bugs at this time.

### Contact Info
Email <jamescarlosborn@gmail.com> with any bug reports or feedback

### License
This application usese the Mit License.
