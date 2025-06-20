# oop_pw1_ext_2425
This repository is the base element for the development of Practice 1 for the extraordinary OOP exam session. 

## Design Detailed Document

### Table of Contents
- [Introduction](#1-introduction)
- [Description](#2-description)
- [Problems](#3-problems)
- [Conclusions](#4-conclusion)

### Introduction

This program simulates the operation of a train station, managing the arrival and docking of different trains. It uses object-oriented programming concepts, such as encapsulation or inheritance in C#.

### Description

#### Class Diagram
![Class Diagram](./ClassDiagram.png)
This class diagram shows all the classes and methods used for the train station.

#### Train
The Train class has an ID, an arrival time, the type (Passenger or freight), and the enum of the train status (EnRoute, Waiting, Docking and Docked). It doesn't have any methods

#### Freight Train
The FreightTrain class is derived from Train, and adds two specific attributes, the maximum weight and the freight type. It doesn't have any methods.

#### Passenger Train
The PassengerTrain class is derived from Train, and also adds two specific attributes, the number of carriages and the capacity. It doesn't have any methods.

#### Platform
The Platform Class has an enum status (Free or occupied), an ID, a docking time, and a declaration of Train used for the operations (currentTrain). It doesn't have any methods

#### Station
The Station Class is the most important because it does all the operations. It creates a list of trains and platforms, and a name for the station.

The DisplayStatus method shows the information of every train, its ID, status and arrival time. It also shows information about the platform, wether it is free or occupied, nad what train occupies it. This is made using two foreach loops.

The LoadTrainsFromFile method first asks for the name of the csv file, then, using streamreader, adds every train to the list, and differenciates if its a passenger or a freight train.

The AdvanceTick method