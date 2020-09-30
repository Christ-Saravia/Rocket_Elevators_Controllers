class MyDisplay:
    def __init__(self, quelEtageOnEst, ascenseurA, ascenseurB, noFloor):
        self.qEo = quelEtageOnEst
        self.asA = ascenseurA
        self.asB = ascenseurB
        self.noF = noFloor
    
    def monEcran(self):
        ecran = ["X"] * self.noF, ["X"] * self.noF, [None] * self.noF
        ecran[1][2] = "!"
        iterator = self.noF
        position = 1
        while iterator > 0:
            print(iterator)
            ecran[2][position - 1] = position
            position += 1
            iterator -= 1
        for v in ecran:
            print(v)


buddah = MyDisplay(3, 5, 8, 10)
buddah.monEcran()