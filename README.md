# F1Tenth AR application
This application is built for IDLab by intern Wouter Huygen from Artesis Plantijn as a bachelors thesis.

## How to read the code
The application code can be devided mainly in 3 parts, the augmentation part, the configuration part and the UI part. All code is writtin in a component based manner with a bit of object oriënted principles because of the way that Unity works.

You can find all scripts in the 'Assets' folder under [Scripts](https://github.com/WouterHuygen/f1tenth-AR-application/tree/master/Assets/Scripts). Here you can find 3 more folders:
* The **AR Scripts** folder contains all scripts regarding **NetMQ**, **Protobuff** and **Vuforia**.
* The **Configuration Scripts** folder is all about the reading and writing from the **XML** settings files.
* And last but not least the **UI Scripts** folder contains scripts that build the UI functionality.

### Augmentation
* The most important script of the entire project is the [Vehicle Controller](https://github.com/WouterHuygen/f1tenth-AR-application/blob/master/Assets/Scripts/AR%20Scripts/VehicleController.cs) script. In this script the most logic is done for the application. It uses the **NetMqListener**, **Pose** and the **PoseConverter** classes.

* The [Net MQ Listener](https://github.com/WouterHuygen/f1tenth-AR-application/blob/master/Assets/Scripts/AR%20Scripts/NetMqListener.cs) script handles the position updates from the backend vehicle simulator.

* The **Pose** script is an automatically generated class by Google's Protocol Buffers. It is based on a **.proto** file.

* The [Pose Converter](https://github.com/WouterHuygen/f1tenth-AR-application/blob/master/Assets/Scripts/AR%20Scripts/PoseConverter.cs) script is used to convert the proto generated Pose to the built-in Unity Pose.

* The [Camera Focus Controller](https://github.com/WouterHuygen/f1tenth-AR-application/blob/master/Assets/Scripts/AR%20Scripts/CameraFocusController.cs) script is a small script that makes sure the AR camera recovers it's focus after loosing it.

### Configuration
* The [SettingsManager](https://github.com/WouterHuygen/f1tenth-AR-application/blob/master/Assets/Scripts/Configuration%20Scripts/SettingsManager.cs) class is a **Singleton** class that persists throughout the entire lifecycle of the app. It is accessible in every class and it holds all configuration settings. It has a few methods for reading and writing to a specific XML config file.

* [Add New Settings](https://github.com/WouterHuygen/f1tenth-AR-application/blob/master/Assets/Scripts/Configuration%20Scripts/AddNewConfig.cs) script written to handle the **AddNewSettings Scene** logic.

* [Edit Current Settings](https://github.com/WouterHuygen/f1tenth-AR-application/blob/master/Assets/Scripts/Configuration%20Scripts/EditCurrentConfig.cs) script written to handle the **EditCurrentSettings Scene** logic.

* [Load Config Files](https://github.com/WouterHuygen/f1tenth-AR-application/blob/master/Assets/Scripts/Configuration%20Scripts/LoadConfigFiles.cs) script written to handel the **LoadOtherSettings Scene** logic.

### UI
These scripts are put on Unity's UI components and are largely self explanatory.


