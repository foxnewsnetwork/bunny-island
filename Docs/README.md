## Tutorial Learning

Currently going through https://www.youtube.com/watch?v=2dsiydjNzLQ

## Aug 20

Game structure, potentially, the game "hierarchy" could look  something like this

- TitleScene
  - TBD
- GameScene
  - GameRules
    - (prevState, event) => nextState
    - ...
  - PlayerInterface
    - ConstructionManager isFocused={focus ==  'construction'} 
      - LineBuilder
      - BoxBuilder
      - FieldBuilder
      - DragDropBuilder
    - MenuManager isFocusesd={focus == 'menu'}
    - DefaultManager isFocused={focus == 'default'}

## Aug 14

Going to try to generalize building into the following 4 types

- Linear  building
  - aka roads, trenches, etc
- Box buildings
  - walls, fences, and other things that go "between" tiles
- area buildings
  -  Zones, fields, etc
- drag n drop
  - npc, preset buildings, etc

## July 30

Continuing with P6 of tutorial

https://www.youtube.com/watch?v=KihjKSUcMXU&t=1s
