# [Unity/C#] Tutorials

**Mina Pêcheux - Since November 2022**

<img style="width: 128px; margin: 1rem 0;" src="doc/unity-square.png" />

This repo contains the code and assets for the various Unity/C# tutorials I published as texts/videos on YouTube and Medium (🇬🇧 + 🇫🇷).

---

As of now, the repo contains some global assets/settings, and personal libraries (in the `Assets/Dependencies/` folder), like:

- `BehaviorTree`: A basic C# library implementing abstract node and tree structures to create behaviour trees. You can see an example of usage in [Tutorial 01: Using behaviour trees for a RTS collector AI](#tutorial-01_bts-rts).
- `AStar`: A basic C# library with A* pathfinding algorithms. For now, it's only for 2D tilemaps - the code was adapted from [this great Github](https://github.com/pixelfac/2D-Astar-Pathfinding-in-Unity) by pixelfac.

---

And in the `Assets/` folder, you'll find the code and assets for the following tutorials:

## 01. Using behaviour trees for a RTS collector AI [<div />](#tutorial-01_bts-rts)

Discover how to use the behaviour tree AI design pattern to give some life to RTS collector units! These little trucks will chop down trees or mine ores to gather wood and minerals, auto-finding the closest targets and regularly delivering their resources to a nearby depot, until the entire map is empty...

- Watch on YouTube: [in English 🇬🇧](https://www.youtube.com/watch?v=ySIzNaW0HUI), [in French 🇫🇷](https://www.youtube.com/watch?v=utbQapz6DoU)
- Read on [Medium](https://mina-pecheux.medium.com/using-behaviour-trees-for-a-rts-collector-ai-in-unity-c-dca24243ebce)
- See the files [in the repo](/Assets/01-BehaviourTreesRTS/)

![cover-01_behaviour-trees_rts](doc/01_behaviour-trees_rts.gif)
