Friend Module Names
    Friend ReadOnly Goblin As New List(Of String) From
        {
            "Gug",
            "Flarg",
            "Febs",
            "Hobs",
            "Pris",
            "Xerk",
            "Biq",
            "Slorkiank",
            "Friesiel",
            "Crilorm",
            "Pliagnubs",
            "Catar",
            "Gnyq",
            "Sraard",
            "Prirm",
            "Urt",
            "Bryc",
            "Xirzerx",
            "Villalk",
            "Cluigtets",
            "Slykloz",
            "Gnovnyrm",
            "Polk",
            "Ilm",
            "Thrilx",
            "Ohai",
            "Tinai",
            "Woimvil",
            "Alrialmi",
            "Blyrsohx",
            "Gnabneah",
            "Brapsyft"
        }
    Friend ReadOnly Orc As New List(Of String) From
        {
"Orgoth",
"Yegigoth",
"Qog",
"Hugolm",
"Rurigig",
"Rurigig",
"Zudagog",
"Sabub",
"Gujjab",
"Brokil",
"Kegigoth",
"Jreghug",
"Krouthu",
"Hrolkug",
"Gunaakt",
"Gnarg",
"Nehrakgu",
"Ouhgan",
"Mughragh",
"Burguk",
"Grazob",
"Mogak",
"Glob",
"Bolar",
"Ghob",
"Badbog",
"Bogdub",
"Ugor",
"Shel",
"Rulfim"
        }
    Friend ReadOnly Human As New List(Of String) From
        {
"Jake",
"Arthur",
"Norman",
"Jonathan",
"Darren",
"Daniel",
"Joel",
"Henry",
"Jesse",
"Jalen",
"Alexander",
"Samuel",
"Andrew",
"Homer",
"Steve",
"Albert",
"Oscar",
"Charles",
"Alex",
"Javier",
"Peggy",
"Maude",
"Patricia",
"Arlene",
"Sue",
"Wanda",
"Ann",
"Susan",
"Faith",
"Zoey",
"Fritz"
        }
    Friend ReadOnly Fish As New List(Of String) From
        {
            "Thazzi",
            "Thiksukh",
            "Xessucketh",
            "Ecicoss",
            "Odjizha",
            "Lula",
            "Ada",
            "Chirkettra",
            "Midaya",
            "Chishtrashthashma",
            "Critsher",
            "Roshet",
            "Yicirge",
            "Szussiezhoth",
            "Oxhizsa",
            "Handha",
            "Vobha",
            "Mihutha",
            "Krashputtu",
            "Umvrupavya",
            "Seexaix",
            "Darga",
            "Artolhis",
            "Croskieciz",
            "Aathizsi",
            "Amat",
            "Dhrasras",
            "Hrishoylu",
            "Kavyadma",
            "Tabhavosa"
        }

    Friend Function GenerateQuestGiverName() As String
        Return $"{RNG.FromList(Human)} the {RNG.FromList(QuestGiverProfession)}"
    End Function

    Friend ReadOnly DungeonNoun As New List(Of String) From
        {
            "Dungeon",
            "Tomb",
            "Crypt",
            "Mausoleum",
            "Vault",
            "Obliette",
            "Catacomb",
            "Sepulcher",
            "Pit",
            "Rootcellar",
            "Stronghold",
            "Bastion",
            "Citadel",
            "Fortress",
            "Bunker",
            "Lair"
        }
    Friend ReadOnly DungeonAdjective As New List(Of String) From
        {
            "Evil",
            "Deadly",
            "Sinister",
            "Fell",
            "Cursed",
            "Corrupt",
            "Wicked",
            "Vile",
            "Malevolent",
            "Hideous",
            "Foul",
            "Villainous",
            "Mighty",
            "Loathsome",
            "Condemned",
            "Dark",
            "Aggressive",
            "Cruel",
            "Creepy",
            "Spooky",
            "Filthy",
            "Grotesque",
            "Defiant"
        }
    Friend ReadOnly DungeonOwner As New List(Of String) From
        {
            "Necromancer",
            "Wizard",
            "Cultist",
            "Summoner",
            "Witch",
            "Warlock",
            "Occultist",
            "Sorcerer",
            "Warlord",
            "Lemur",
            "Mutant",
            "Wyvern",
            "Witch Doctor",
            "Shaman",
            "Bureaucrat"
        }
    Friend Function GenerateDungeonName() As String
        Return $"{RNG.FromList(DungeonNoun)} of the {RNG.FromList(DungeonAdjective)} {RNG.FromList(DungeonOwner)}"
    End Function
    Friend ReadOnly ShoppeAdverb As New List(Of String) From
        {
            "Uncannily",
            "Obsequiously",
            "Cheerfully",
            "Elegantly",
            "Warmly",
            "Honestly",
            "Gracefully",
            "Casually",
            "Gradually",
            "Instantly",
            "Accidentally",
            "Seriously",
            "Nervously",
            "Woefully"
        }
    Friend ReadOnly ShoppeAdjective As New List(Of String) From
        {
            "Happy",
            "Friendly",
            "Sketchy",
            "Wholesome",
            "Fair",
            "Convenient",
            "Adorable",
            "Attractive",
            "Charming",
            "Delightful",
            "Exuberant",
            "Jolly",
            "Pleasant",
            "Quaint",
            "Splendid",
            "Strange",
            "Eager"
        }
    Friend ReadOnly ShoppeNoun As New List(Of String) From
        {
            "Shoppe",
            "Boutique",
            "Emporium",
            "Market",
            "Outlet",
            "Store",
            "Bazaar",
            "Depot",
            "Arsenal",
            "Warehouse",
            "Armory",
            "Mart"
        }
    Friend Function GenerateShoppeName() As String
        Return $"{RNG.FromList(ShoppeAdverb)} {RNG.FromList(ShoppeAdjective)} {RNG.FromList(ShoppeNoun)}"
    End Function
    Friend ReadOnly QuestGiverProfession As New List(Of String) From
        {
            "Harlot",
            "Innkeeper",
            "Gong Farmer",
            "Mudlark",
            "Tosher",
            "Knacker",
            "Peasant",
            "Midwife",
            "Plague Doctor",
            "Wetnurse"
        }
End Module
