# **GoogleNews**
#### web aplication 
#### Displays topics from the RSS feed of Google News, 
#### by clicking on a topic - the name, content, link to the full article opens,

## Link Screen Recording
  https://drive.google.com/file/d/1LpP0qQFTprCfj58kMEChg9I3GaodHKhV/view
  
## The Technological Languages:
The project write by ASP.Net Core Technologic.
Two project in solution divided to two layers DAL and UI.

### The Structure Of The Project.
 - DAL
     Project in type Class Library.
     Contain:
       -Models folder: Within it, a class named Item.
       -Services: A file named GoogleNewsDAL that performs external API calls and interacts with HttpCache.
       -LogUnity: class of log used to document the errors.
 - UI
     Project in type ASP.NET Core Web API.
     Contain:
       -Controllers folder: Within it, The layer that receives the API calls and directs to NewsService to perform the required actions.
       -wwwroot: Contains files external to the UI (HTML, CSS, JS)
           - I defined that the program will be launched from there and not Swagger

### Technologies Used In The Project:
  - AJAX, jQuery:
      Technologic to called API from JS.
      This Technologic allows web pages to dynamically update content without requiring full page reloads.
      It improves the user experience by providing faster and more interactive browsing.
      AJAX reduces server load by retrieving data asynchronously, leading to improved performance.
    
  - HttpCache:
      saving data in HttpCache for higher performance speed.
      An external API call that takes time will only happen once at a certain time defined in the project.
      And the other times the information will be read from the cache.
    
  - Repeater control:
      That after a thorough examination it became clear that this technology is old and is not supported in .net Core 7.
      A parallel operation with the same meaning was performed in JS.

### The project created by Tamar Aharoni
