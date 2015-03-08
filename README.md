# BigNumber

A data structure that uses a linked list to represent a number that can be greater than the largest integer.

## About

This is a class that uses a linked list to represent a number composed of digits 0-9 in each node. Therefore the length of the integer is only limited to the size of a linked list.

## Features

**Supports:**

- **Explicit value** initialization by passing a value to the constructor.
- **Implicit value** initialization (BigNumber num = 100).
- Basic math operations: **addition, subtraction, multiplication, and division**.
- These operations are also supported by **operator overloads** (num *= 10).
- Simple **formatting** (e.g. 1000) and number formatting (e.g. 1,000).

**Lacks:**

- Decimal point support.
- More flexible formatting options.

## Why?

I just had the idea one day and thought it would be a fun exercise. I wondered if I could make it perform well, even with large numbers. If you see *any* room for improvement, please fork and contribute!