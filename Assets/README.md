[TOC]

# MGS.Undo

## Summary

- Unity plugin for undo and redo develop.

## Environment

- Unity 2021.3 or above.

## Usage

- Create a Global Instance for IUndoManager.
- Register a instance of IDoHandler (example: DoHandler) to the Global Instance when you need record to undo and redo.
- Call the Undo or Redo method of Global Instance.

------

Copyright © 2025 Mogoson.	mogoson@outlook.com