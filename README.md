# GameDevProject
Adrian Dyszczak

movementManager from Hero Movement: Benefits of This Refactor:
Single Responsibility Principle (SRP):

The Hero class is now focused only on coordinating its own behavior (e.g., managing animations, shooting, and rendering).
The MovementManager is solely responsible for movement logic.
Open/Closed Principle (OCP):

If you want to change movement speed, implement acceleration, or add special movement behaviors, you can modify the MovementManager class without touching the Hero class.

we houden simpel interface want Adding more methods or parameters would defeat the purpose of the Interface Segregation Principle (ISP).++ interface movement: The IMovementBehaviour interface could be a valuable addition to your project, particularly if you're aiming to create a flexible and scalable system for movement mechanics. Its purpose aligns with the Strategy Pattern, which allows you to define different movement behaviors and switch between them dynamically.