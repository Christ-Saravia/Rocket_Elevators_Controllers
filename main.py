




class Column:
    def __init__(self, num_floor, elevator_number, positionAscenseurA, positionAscenseurB, minimumFloor, maximumFloor):
        self.num_floor = num_floor
        self.elevator_number = elevator_number
        self.positionAscenseurA = positionAscenseurA
        self.positionAscenseurB = positionAscenseurB
        self.minimumFloor = minimumFloor
        self.maximumFloor = maximumFloor

    def createElevatorList(self):
        elevatorList = [None] * self.elevator_number, [None] * self.elevator_number
        elevatorList [0][elevator_number - 1] = positionAscenseurB
        elevatorList [0][elevator_number - 2] = positionAscenseurA
        elevatorList [1][elevator_number - 1] = elevator_number
        elevatorList [1][elevator_number - 2] = elevator_number - 1
        for x in elevatorList:
            print(x)

# creation de button haut et bas pour chaque etage 

    def createButtonList(self):
        buttonList = ['UP'] * self.num_floor , ['DOWN'] * self.num_floor
        buttonList[self.elevator_number - self.elevator_number][self.minimumFloor - 1] = 'DISABLE'
        buttonList[self.elevator_number - 1][self.maximumFloor - 1] = 'DISABLE'
        for w in buttonList:
            print(w)

#Sa cest ce que l'utilisateur vont voir a l'interieur de l'ascenseur

    def createPanelList(self):
        panelList = [None] * self.num_floor
        iterator = self.num_floor
        while iterator > 0:
            panelList[iterator - 1] = iterator
            iterator -= 1
        print(panelList)

#
class Elevator:
    def __init__(self, num_floor, ux, status):
        self.num_floor = num_floor
        self.ux = ux
        self.status = status
    
    def createDoorsList(self):
        doorsList = ['CLOSED'] * self.num_floor
        doorsList[self.status] = ux
        print(doorsList)


#ouvrir ou fermer les portes
class Doors:
    def __init__(self, status):
        self.status = status
    
    def doorsControl(self):
        return 'OPENED'

#appel un ascenseur ou signaler qu'un etage demande un ascenseur 
class Button:
    def __init__(self, num_floor):
        self.floor = num_floor
    
    def createAppelButtonList(self):
        appelButtonList = ['OFF'] * self.floor
        print(appelButtonList)


f = Column(10, 2, 2, 6, 1, 10)
f.createElevatorList()
f.createPanelList()
f.createButtonList()

y = Column(num_floor, elevator_number, positionAscenseurA, positionAscenseurB, minimumFloor, maximumFloor)
y.createElevatorList()

j = y
j.createPanelList()

w = y
w.createButtonList()

u = Doors(num_floor)
ux = u.doorsControl()

o = Elevator(num_floor, ux, status)
o.createDoorsList()

k = Button(num_floor)
k.createAppelButtonList()









    












