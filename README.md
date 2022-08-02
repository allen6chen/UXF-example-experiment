<p align="center"><img src="img/0_pick_place.gif"/></p>

# UXF Tutorial

UXF-Unity Experiment Framework enables users to conduct human behaviour experiments in Unity Engine including VR, desktop and Web baed experiments.([http://wiki.ros.org/ROS/Introduction](https://github.com/immersivecognition/unity-experiment-framework)) (UXF) This tutorial will go through the steps necessary to integrate UXF with Unity, from installing the Unity Editor to creating a scene with an imported package to completing a ... task.
This tutorial is designed such that you do not need prior experience with Unity or C# in order to follow the scene setup steps, and you do not need prior robotics experience to get started with ROS integration. The tutorial is divided into high-level phases, from basic Unity and ROS initial setup through executing a pick-and-place task.

**Want to skip the tutorial and run the full demo? Check out our [Quick Demo](quick_demo.md)**

---

We're currently working on lots of things! As a first step for this tutorial, please take a short moment fill out our [survey](https://unitysoftware.co1.qualtrics.com/jfe/form/SV_0ojVkDVW0nNrHkW) to help us identify what products and packages to build next.

---

### Pick-and-Place Tutorial
  - [Requirements](#requirements)
  - [Part 0: UXF Setup](#UXF-setup)
  - [Part 1: Create Unity scene with imported UXF](#part-1-create-unity-scene-with-imported-urdf)
  - [Part 2: ](#part-2-)
  - [Part 3: ](#part-3-)
  - [Part 4: ](#part-4-)

## Requirements

This repository provides project files for the tutorial, including Unity assets, URDF files, and ROS scripts. Clone this repository to a location on your local machine:
  ```bash
  
  ```

## [Part 0: ROS Setup](0_ros_setup.md)

<img src="img/0_docker.png" width="400"/>

This part provides two options for setting up your ROS workspace: using Docker, or manually setting up a catkin workspace.

## [Part 1: Create Unity scene with imported URDF](1_urdf.md)

<img src="img/1_end.gif" width="400"/>

This part includes downloading and installing the Unity Editor, setting up a basic Unity scene, and importing a robot--the [Niryo One](https://niryo.com/niryo-one/)--using the URDF Importer.

## [Part 2: ](2_ros_tcp.md)

<img src="img/2_echo.png" width="400"/>

This part covers creating a TCP connection between Unity and ROS, generating C# scripts from a ROS msg and srv files, and publishing to a ROS topic.

## [Part 3: ](3_pick_and_place.md)

<img src="img/0_pick_place.gif" width="400"/>

This part includes the preparation and setup necessary to run a pick-and-place task with known poses using MoveIt. Steps covered include creating and invoking a motion planning service in ROS, moving a Unity Articulation Body based on a calculated trajectory, and controlling a gripping tool to successfully grasp and drop an object.

## [Part 4: ](4_pick_and_place.md)

<img src="img/4_pick_and_place.gif" width="400"/>

This part is going to be a little different than the previous tutorials in that it will utilize a real Niryo One robot. We do not assume that everyone has access to a Niryo One outside of simulation. As such this tutorial should mostly be used as a reference for how to move from executing commands on a simulated robot to a real one.
