from playsound import playsound

num_floor = 10
elevator_number = 2
positionAscenseurA = 2
positionAscenseurB = 6
minimumFloor = 1
maximumFloor = num_floor
status = 0      #0 = false pour le moment et 1 = true  
floorWhereRequestWasMade = 3
 



class Column:
    def __init__(self, num_floor, elevator_number, positionAscenseurA, positionAscenseurB, minimumFloor, maximumFloor,floorWhereRequestWasMade):
        self.num_floor = num_floor
        self.elevator_number = elevator_number
        self.positionAscenseurA = positionAscenseurA
        self.positionAscenseurB = positionAscenseurB
        self.minimumFloor = minimumFloor
        self.maximumFloor = maximumFloor
        self.floorWhereRequestWasMade = floorWhereRequestWasMade

    def createElevatorList(self):
        elevatorList = [None] * self.elevator_number, [None] * self.elevator_number
        elevatorList [0][elevator_number - 1] = self.positionAscenseurB
        elevatorList [0][elevator_number - 2] = self.positionAscenseurA
        elevatorList [1][elevator_number - 1] = self.elevator_number
        elevatorList [1][elevator_number - 2] = self.elevator_number - 1
        joe = elevatorList [1][elevator_number - 2]
        martin = elevatorList [1][elevator_number - 1] 
        return joe, martin

# creation de button haut et bas pour chaque etage a l'exterieur des ascenseurs

    def createButtonList(self):
        buttonList = ['<UP>'] * self.num_floor , ['<DOWN>'] * self.num_floor
        buttonList[self.elevator_number - self.elevator_number][self.maximumFloor - 1] = 'DISABLE'
        buttonList[self.elevator_number - 1][self.minimumFloor - 1] = 'DISABLE'
        print('Here are the button(s) available on your floor \n')
        if buttonList[0][floorWhereRequestWasMade - 1] != 'DISABLE':
            print(buttonList[0][floorWhereRequestWasMade - 1])
        if buttonList[1][floorWhereRequestWasMade - 1] != 'DISABLE':
            print(buttonList[1][floorWhereRequestWasMade - 1])
        print('\n')
        
#Sa cest ce que l'utilisateur vont voir a l'interieur de l'ascenseur

    def createPanelList(self):
        panelList = [None] * self.num_floor
        iterator = self.num_floor
        while iterator > 0:
            panelList[iterator - 1] = iterator
            iterator -= 1
        print('Voici panel list: ')
        print(panelList)
        return panelList

def createElevatorTrack(jersey,vicky):
    elevatorTrackA = [None] * num_floor
    elevatorTrackB = [None] * num_floor
    elevatorTrackA[positionAscenseurA - 1] = jersey
    elevatorTrackB[positionAscenseurB - 1] = vicky
    return elevatorTrackA,elevatorTrackB


#
class Elevator:
    def __init__(self, num_floor, ux, status, floorWhereRequestWasMade, elevatorTrackA,elevatorTrackB, positionAscenseurA, positionAscenseurB):
        self.num_floor = num_floor
        self.ux = ux
        self.status = status
        self.requestWasMade = floorWhereRequestWasMade
        self.elevatorTrackA = elevatorTrackA
        self.elevatorTrackB = elevatorTrackB
        self.positionAscenseurA = positionAscenseurA
        self.positionAscenseurB = positionAscenseurB

    def createDoorsList(self):
        doorsList = ['CLOSED'] * self.num_floor, ['CLOSED'] * self.num_floor
        doorsList[self.status][self.status] = ux
        print('\n Here is the state of the doors on your floor as seen from the outside presently\n')
        doorTrackA = doorsList[0][self.requestWasMade - 1]
        doorTrackB = doorsList[1][self.requestWasMade - 1]
        return doorTrackA, doorTrackB


    def createMoveUp(self):
        boundPickupA = (self.positionAscenseurA -1)-(floorWhereRequestWasMade - 1)
        boundPickupB = (self.positionAscenseurB - 1)-(floorWhereRequestWasMade - 1)
        if abs(boundPickupA) < abs(boundPickupB):
            iterator = abs(boundPickupA)
            while iterator > 0:
                lastPositionElevatorA = self.elevatorTrackA.index(1)
                print(self.elevatorTrackA)
                playsound('timer.wav')
                newPositionElevatorA = lastPositionElevatorA + 1
                self.elevatorTrackA[lastPositionElevatorA] = None
                self.elevatorTrackA[newPositionElevatorA] = 1
                print(self.elevatorTrackA)
                iterator -= 1
                return self.elevatorTrackA.index(1)

    def goToFloor(self):
        moveUp = 1
        moveDown = -1 
        etageDesire = 8
        difference = (etageDesire - 1) - (floorWhereRequestWasMade - 1)
        if difference > 0:
            move = moveUp
        else:
            move = moveDown
        iterator = difference * move
        while iterator > 0:
            playsound('exit_cue_y.wav')            
            lastPositionElevator = self.elevatorTrackA.index(1)
            self.elevatorTrackA[lastPositionElevator] = None
            self.elevatorTrackA[lastPositionElevator + 1] = 1
            print(self.elevatorTrackA)
            if iterator == 1:
                playsound('yay_z.wav')
                print("Thank you for using Rocket Elevator!")
            iterator -= 1
            


#ouvrir ou fermer les portes
class Doors:
    def __init__(self, status):
        self.status = status
    
    def doorsControl(self):
        if self.status == 1:
            doorsStatus = 'OPENED'
        else:
            doorsStatus = 'CLOSED'
        return doorsStatus

#appel un ascenseur ou signaler qu'un etage demande un ascenseur 
class Button:
    def __init__(self, num_floor,floorWhereRequestWasMade, status , direction):
        self.floor = num_floor
        self.signal = floorWhereRequestWasMade - 1
        self.status = status
        self.direction = direction

    
    def requestElevator(self):
        appelButtonList = ['OFF'] * self.floor
        if self.status == 1:
            appelButtonList[self.signal] = 'ON'
        return appelButtonList
    
def searchElevator(arr, x):
    for buddah, value in enumerate(arr):
        if value == x:
            return buddah + 1



#pour le moment je le fais comme une class, mais va etre une fonction que je vais mettre a l'interieur d'une class
class GoTo:
    def __init__(self, nom):
        self.nom = nom
    def commencerLeProgramme(self):
        print('Do you want to go up or down? \n')
        print('<Up> = Enter the key U \n<Down> = Enter the key D \n')
        actionButton = print('Enter your key:'), input()
        if actionButton == 'u' or 'U':
            print("You decided to go upstairs\n")
            floorWhereRequestWasMade = self.nom
            status = 1
            return status, floorWhereRequestWasMade, actionButton

"""
 JE VAIS FAIRE DES TESTS
 SCENARIO A
 
 ELEVATOR A EST INACTIF AU 2E ETAGE
 ELEVATOR B EST INACTIF AU 6E ETAGE
 Someone is on floor 3 and wants to go to the 7th floor. 
 Elevator A is expected to be sent.
"""
print(
    '\n SCENARIO A : \n-Elevator A is on the 2nd floor \n-Elevator B is on the 6th floor \n You are on the 3rd floor and you want to go to the 7th floor. \n'
    )

scenarioA = Column(10,2,2,6,1,10,floorWhereRequestWasMade)
scenarioA.createButtonList()


g = GoTo(3)
monday = g.commencerLeProgramme()
arrayID = scenarioA.createElevatorList()
direction = monday[2]

k = Button(num_floor,floorWhereRequestWasMade,monday[0], direction)

k.requestElevator()

manon = createElevatorTrack(arrayID[0],arrayID[1])
  
u = Doors(status)
ux = u.doorsControl()


o = Elevator(num_floor, ux, status, floorWhereRequestWasMade, manon[0], manon[1],positionAscenseurA,positionAscenseurB)
portes = o.createDoorsList()
portesA = portes[0]
portesB = portes[1]
print("p1: ", portesA)
print("p2: ", portesB)



johanne = o.createMoveUp()
if johanne == floorWhereRequestWasMade - 1:
    d = Doors(1)
    ux = d.doorsControl()
    changement = ux
    o = Elevator(num_floor, ux, status, floorWhereRequestWasMade, manon[0], manon[1],positionAscenseurA,positionAscenseurB)
    portes = o.createDoorsList()
    portesA = portes[0]
    portesA = changement
    portesB = portes[1]
    playsound('boxing_bell.wav') 
    print("[ring! ring ! The elevator has just arrived at the request floor]\n ")
    print("p1: ", portesA)
    print("p2: ", portesB)
    playsound('traffic_city.wav')
    d = Doors(0)
    ux = d.doorsControl()
    changement = ux
    o = Elevator(num_floor, ux, status, floorWhereRequestWasMade, manon[0], manon[1],positionAscenseurA,positionAscenseurB)
    portes = o.createDoorsList()
    portesA = portes[0]
    portesA = changement
    portesB = portes[1]
    print("p1: ", portesA)
    print("p2: ", portesB) 

o.goToFloor()








