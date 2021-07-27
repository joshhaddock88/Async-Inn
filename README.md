# Async-Inn

> A database for an imaginary hotel chain.

By: Joshua Haddock

## Table of Contents

* [General Info](#general-information)
* [Technologies Used](#technologies-used)
* [Features](#features)
* [Media](#media)
* [Table Explanation](#table-explanation)
* [Setup](#setup)
* [Usage](#usage)
* [Project Status](#project-status)
* [Room for Improvement](#room-for-improvement)
* [Acknowledgements](#acknowledgements)
* [Contact](#contact)
* [License](#license)

## General Information

* For the first part of this lab we are simply creating and ERD model for a made up hotel chain. This a practice in designing the architecture of relational databases.

## Technologies Used

* C# 9.0
* .Net 5

## Features

* A completed ERD diagram.

## Media

![ERD Model](./images/AsyncInn.png)

## Table Explanation

### Location

Locations is a class which holds the properties Name, City, State, Address, and PhoneNumber. All of these are VarChar except PhoneNumber, which is an int.

### Bookings

The location feeds into "Bookings" which gives bookings composite keys. Bookings will take in All rooms and the location, and kit keeps track of how many units exist in the hotel, and how many are currently available. It is also being fed from "Specials" which keeps track of all sales etc going on.

### Specials

Specails has only a primary key and gives out the amount of discount and the start and end date of the special.

### Room

Room takes in from the enum list "Amenities" and "Room types". It has a nickename property, it's amenities, a boolean telling whether or not it is reserved (which is fed into "Bookings").

### Room Types

Room types has the properties of room style, popularity, and size. It feeds all that information into Room via a foreign key.

### Amenities

Amenities is an enurable list which connects to rooms. It consists primarily of booleans which will tells us either true or false if a room has a specific amenity.

## Setup

## Usage

## Project Status

In Progress

## Room for Improvement

To do:

## Acknowledgements

## Contact

Created by [Joshua Haddock](https://www.linkedin.com/in/joshuahaddock/) - feel free to contact me!

## License

This project is open source and available under the [MIT License](./LICENSE).

