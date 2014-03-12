/* TODO: Maybe reset data so we won't fail on foreign key constraints? */

/*ATTRIBUTES*/
REPLACE INTO Attribute VALUES (1, 'Damage');
REPLACE INTO Attribute VALUES (2, 'Health');
REPLACE INTO Attribute VALUES (3, 'Speed');
REPLACE INTO Attribute VALUES (4, 'Range');
REPLACE INTO Attribute VALUES (5, 'Capacity');

/*SLOTS*/
REPLACE INTO Slot VALUES (1, 'Hands'); /* Let's just have 1 slot for weapons */
/*ITEM TYPES*/
REPLACE INTO Item_Type VALUES (1, 'Money');
REPLACE INTO Item_Type VALUES (2, 'Weapon');
REPLACE INTO Item_Slot VALUES (2, 1); /* Weapons can go in Hands slot */

/*ITEMS*/
/*Gold*/
REPLACE INTO Item VALUES(1, 1, 'Gold', 'Used to purchase things');
/*Laser Weapon*/
REPLACE INTO Item VALUES(2, 2, 'LaserWeapon', 'A weapon that fires a laser');
REPLACE INTO Item_Attributes VALUES (2, 1, 1);
REPLACE INTO Item_Attributes VALUES (2, 4, 20);

/*RECIPES*/
/*Purchase Laser Weapon for 20 gold*/
REPLACE INTO Recipe VALUES (1, ""); /*What would we put as the name? */
REPLACE INTO Recipe_In VALUES (1, 1, 10);
REPLACE INTO Recipe_Out VALUES (1, 2, 1);