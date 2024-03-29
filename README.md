# Facebook Chat Statistics

Windows app to analyze Facebook chat data

## Introduction

I created this while being in bed with COVID. 
It was a fun project, feel free to use it.

The idea is that you feed it your messenger chat data and it shows the people you chatted most with through time.

## Instructions

### 1. Request data from Facebook

* Go to Facebook on web and navigate to Your Facebook information

![downward triangle](https://github.com/AndreCL/FacebookChatStatistics/blob/master/Screenshots/01-Screenshot.jpg?raw=true)

![Settings & privacy](https://github.com/AndreCL/FacebookChatStatistics/blob/master/Screenshots/02-Screenshot.jpg?raw=true)

![Settings](https://github.com/AndreCL/FacebookChatStatistics/blob/master/Screenshots/03-Screenshot.jpg?raw=true)

![Your Facebook Information](https://github.com/AndreCL/FacebookChatStatistics/blob/master/Screenshots/04-Screenshot.png?raw=true)

* Click on "View" on "Download your information"
* In "Request a download"
* Set Format to JSON
* Media quality to "Low". I wish there was an option not to download media, which we don't use.
* Date Range to "All time". If you just wish to check for the last year, just download for the last year.
* In select information to downlad, click "Deselect all"

![Request a download](https://github.com/AndreCL/FacebookChatStatistics/blob/master/Screenshots/05-Screenshot.png?raw=true)

* In "Your activity across Facebook", select Messages

![Your activity across Facebook](https://github.com/AndreCL/FacebookChatStatistics/blob/master/Screenshots/06-Screenshot.png?raw=true)

* Click "Request a download"

![Start your download](https://github.com/AndreCL/FacebookChatStatistics/blob/master/Screenshots/07-Screenshot.png?raw=true)

* The site will show that the process is started and you will also receive an email. The process usually takes a few days for me. But I guess it depends on how much data you have.

* After some hours/days you will receive a mail from Facebook that your data is ready for download

![Facebook mail](https://github.com/AndreCL/FacebookChatStatistics/blob/master/Screenshots/10-Screenshot.png?raw=true)

* Click the link and download all the files

![Download data](https://github.com/AndreCL/FacebookChatStatistics/blob/master/Screenshots/11-Screenshot.png?raw=true)

* Unzip all the downloaded files. I recommend using 7-zip

* Inside the messages directory you get there are 5 folders with message threads (each message thread is a folder): 

![Download data](https://github.com/AndreCL/FacebookChatStatistics/blob/master/Screenshots/12-Screenshot.png?raw=true)

* Take the contents of those 5 folders and put them in one folder

* Run the app

* Select directory

* You should see something like this (old version):

![Download data](https://github.com/AndreCL/FacebookChatStatistics/blob/master/Screenshots/13-Screenshot.png?raw=true)

## Todo:

- Implement when user joined (subscribed) later or left (unsubscribed)
- Improve call to only include when people joined/left call
- Make call a line graph instead
- Automatically unzip & more complex find all jsons in all subdirectories
- Include in readme that it requires to install .net 6.0
- Don't lock UI on loading

