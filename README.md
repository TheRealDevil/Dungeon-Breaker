# ⚔️ Dungeon Breaker
Akční 2D hra z pohledem shora, postavená v Unity. Projděte si nebezpečný dungeon, vyberte si svého hrdinu a přežijte střetnutí s bossem, zatímco nasbíráte co nejvyšší skóre.

## 🚀 Funkce

- **Procedurální generování**: Random Walk algoritmus, který generuje unikátní rozvržení dungeonů s místnostmi Start, Enemy a Boss.
- **Dynamická logika místností**: Dveře místností se automaticky zamknou, když hráč vstoupí, a odemknou se až po porážce všech nepřátel.
- **Systém výběru postav**: Vyberte si mezi unikátními třídami postav (Desert Knight a Marksman) s odlišnými statistikami a vizuály řízenými ScriptableObjects.
- **Persistent Game Management**: GameManager sleduje vaše zdraví, skóre a vybranou postavu napříč více scénami a patry.
- **Bojový systém**:
  - Systém projektilů mířených myší s využitím nového vstupního systému.
  - iFrames hráče: Vizuální blikající efekt poskytující dočasnou neporazitelnost po udělení poškození.
  - Více typů nepřátel (bojovníci na blízko a střelci na dálku).
  - Inteligentní umělá inteligence nepřátel: Nepřátelé detekují a pronásledují hráče, když vstoupí do určitého dosahu, s automatickým přepínáním sprite, aby se vždy otočili čelem k cíli.
- **Pohyb založený na fyzice**: 2D pohyb pro hráče a nepřátele pomocí Rigidbody2D.

## 🛠️ Technické detaily

- **Engine**: Unity 6 (6000.0.38f1 LTS)
- **Vstup**: Unity Input System Package
- **Rendering**: 2D Universal Render Pipeline (URP) nebo Standard 2D
- **Skriptování**: C#

## 📂 Struktura projektu

- `BoardManager.cs`: Zvládá generování podlah na základě dlaždicové mapy.
- `DungeonGenerator.cs`: „Mozek“, který mapuje pozice a typy místností.
- `RoomController.cs`: Spravuje stavy jednotlivých místností, zamykání dveří a objevování nepřátel.
- `GameManager.cs` Centrální mozek. Zvládá vysoké skóre, přechody mezi scénami a perzistenci dat mezi scénami.
- `CharacterData.cs` Plán ScriptableObject pro definování statistik postav a ovladačů animátorů.

## 🎮 Jak hrát

- **Stáhnout**: Přejděte do sekce Verze a stáhněte si nejnovější soubor .zip.
- **Rozbalit**: Rozbalte složku na plochu.
- **Spustit**: Dvojitým kliknutím na soubor .exe spusťte hru.

- **Ovládání**
1. **Pohyb**: Klávesy WASD.
2. **Míření**: Kurzor myši.
3. **Střelba**: Mezerník nebo kliknutí levým tlačítkem myši.
4. **Esc**: Návrat do menu
5. **Cíl**: Vyčistěte místnosti, abyste našli místnost s bossem a postupovali do dalšího patra.
