# Luc-Steels-Mars-Explorer-Simulation-In-Unity
Simulation of the Luc Steels' Mars explorer system. This project has been adapted from Steels, L. (1991). "Cooperation between distributed agents through self-organisation".

This project has importance in the field of multiagent sistems: the agents (in this case, robots) are based on the subsumption architecture (cf. Brooks, R.1986. "A robust layered control system for a mobile robot".)

### Why this project?

This project is an ampliation of my other project with the Steels' Mars explorer that you 
can find [here](https://github.com/Amable-Valdes/Luc-Steels-Mars-Explorer-Simulation-with-Collaborative-Transport), 
but instead of been implemented on Javascript i have done it on Unity because i always wanted to do something in that game engine and i wanted to practise my C# skills.

![Simulation image should be visible here](https://github.com/Amable-Valdes/Luc-Steels-Mars-Explorer-Simulation-In-Unity/blob/master/others/Simulation_Example.PNG)

## The project

The Version 1.0 has been implemented. 

- Number of tracks: now each of the tracks has a variable "number of tracks". The tracks start with two and when the robot goes to eliminate a track it decreases this 
variable by one, until the variable reaches 0 and then it is eliminated from the scene.

- Simplified track: Now if a robot places a track near another track, one of these is eliminated and its value of "number of tracks" is transferred to the track not eliminated. 
This simplifies the tracks on the scene, what is translated into a lower number of calculations between collisions and with this we achieve a higher speed in number of frames per second in the simulation.

## Bugs

In the current version I am aware of the existence of two bugs.

- Tracks destroying each others: It is normal that the tracks are simplified (because I have implemented it so), but I have seen a behavior in which both traks are eliminated simultaneously. 
I used a semaphore to solve it but I still have a lot to learn from Unity, I do not know if I have solved it whit this but that's the way it should be.
	- It is not a problem when the simulation is executed. It is a minor problem that is simply a curiosity of Unity's behavior ... And I think that with the last update it is solved.
- The robots stop, begin to vibrate (moving very fast in little space) and do not know what to do: this is a strange error; performing many executions I have found in one of them a situation 
in which the robots can not move, being trapped between the mother ship, the minerals and the tracks... I do not know what can cause this error, although I think it has relation with the dimensions 
of the collisions between robots, mothership and minerals. 
	- It is a very strange bug, hardly reproducible and it is more likely that nobody will ever see it. But I prefer to document it here in case someone ever sees it.
	- In this case the robots do not move and the simulation will never end. Stop the program and start again.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

If you read any bad translation in the project code or in the github documentation send me an 
email with the correct sentences (^.^)

Thanks for read me!