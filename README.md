## Dev Time Tracker

![image](https://user-images.githubusercontent.com/23088305/46271539-0b597100-c51b-11e8-8339-e4afea309665.png)

![image](https://user-images.githubusercontent.com/23088305/46186898-01333a80-c2af-11e8-9da4-4f9806806369.png)

![image](https://user-images.githubusercontent.com/23088305/46186931-29bb3480-c2af-11e8-8419-96d5df6dbc47.png)

### JIRA
To log time to Jira, commit using smart commits. Your commit message will go after the command tag `#comment`.

`ABC-123 #time 2h 15m #comment Fixed Notification #resolve`

- Task id reference : `ABC-123` (can add more than one)
- Log time: `#time 2h 15m` (w: week, d: day, h: hour, m: minute)
- Comment message: `#comment Task completed`
- Resolve issue: `#resolve` (to send all task ids to QA - `#close` to set it to done)
