[TOC]

# MGS.Undo

## Summary

- Unity plugin for undo and redo develop.

## Install

- Unity --> Window --> Package Manager --> "+" --> Add package from git URL...

  ```text
  https://github.com/mogoson/MGS.Undo.git?path=/Assets
  ```

## Usage

- Create a Global Instance for IUndoManager.
- Register a instance of IDoHandler (example: DoHandler) to the Global Instance when you need record to undo and redo.
- Call the Undo or Redo method of Global Instance.

------

Copyright © 2025 Mogoson.	mogoson@outlook.com