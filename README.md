# Wipro
## Xamarin Proficieny Exercise

Completed the provided exercise using Xamarin.Forms PCL application in Android and iOS with all requested functionalities. Please find the more details below.

## Implementation details

### Fetching JSON
First ensured the network connectivity by using the Xam.Plugin.Connectivity open source nuget package and then fetch the json object using HTTP client using Micorsoft.Net.Http nuget package.

### Deserialize JSON
Deserialize the json objects into the classes and properties using the Newtonsoft.Json open source library.

### Scrolling View
Displayed the list of rows in a scrolling view using the listview control by enabling HasUnevenRows support which provides support to have rows with different height based on their content.

Displayed the Title, Description and Images of each item by defining the ItemTemplate of the listview.

### Lazy loading and image caching
Loaded the image lazily and cached the downloaded the image by using the open source library FFImageLoading. So that UI will not stuck while scrolling.

### Refreshing and Sorting
As described in the POC, implemented the refreshing and sorting functionalities in ViewModel and perform action when click on Button by defining Command.
Displayed the activity indicator whenever the fetching data from URL (Initial loading and refreshing).

### UI testing
Created the XamarinUI.Test project to automate some functionalities as described.

Note: Developed application for both Android and iOS. But I have tested only in Android (MI-A1 device). And some images are not available in the provided URLs, so images will not be loaded for those items in application. Only title and description will be displayed. 


## Open Source Libraries used:
<b>Xamarin.Plugin.Connectivity</b> - To check whether device has internet connection or not.<br/>
<b>Newtonsoft.Json</b> - To deserialize the JSON object. <br/>
<b>FFImageLoading</b> - To cache the images to improve the scrolling efficiency.
