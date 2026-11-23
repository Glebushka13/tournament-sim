1. uzdevums
Jāņa enerģija nemainījās, jo anna un janis ir divi dažādi objekti. Katram objektam ir savi lauki, tāpēc, kad es samazināju tikai Annas enerģiju, Jāņa objekts netika skarts.

2. uzdevums
Sākumā Annai bija 100 enerģijas. Pēc ApplyPenalty(30) palika 70, un viņa vēl bija aktīva (true). Pēc ApplyPenalty(100) enerģija kļuva 0, nevis negatīva, un IsActive() atgrieza false.

3. uzdevums
Kad mēģināju ierakstīt anna.energy = 9999;, Build parādīja kļūdu "'Participant.energy' is inaccessible due to its protection level". Tas nozīmē, ka lauks energy ir private, un to drīkst mainīt tikai pati klase Participant. No LessonDemo.cs enerģiju var tikai nolasīt ar GetEnergy() vai mainīt ar ApplyPenalty(). Tā enerģiju nevar nejauši sabojāt.
3.2. stunda

1. uzdevums
Ja Captain konstruktorā izdzēsu : base(name, 150), Build dod kļūdu, jo Participant klasei nav konstruktora bez parametriem. Tāpēc atvasinātajai klasei jānodod vārds un enerģija bāzes konstruktoram ar : base(...).

2. uzdevums
Bez vārda virtual metodē ApplyPenalty klase Captain nedrīkst lietot override, jo bāzes metode nav atļauta pārrakstīšanai. Kapteinim sods 20 atņem 10 (150 → 140), bet sods 6 atņem vismaz 1 (150 → 149).

3. uzdevums
Vienā foreach ciklā nav vajadzīgi atsevišķi if katrai lomai, jo C#" izpildes laikā pats izvēlas katra objekta īsto ApplyPenalty versiju (polimorfisms). Tāpēc parasts dalībnieks zaudē 20, kapteinis 10, prezentētājs 25.
3.3. stunda

1. uzdevums
Participant klasi pārveidoju par Godot mezglu (public partial class Participant : Node). Lauku Name nomainīju uz ParticipantName, lai tas nepārklātos ar Node.Name, un konstruktoru nomainīju ar Setup(), jo Godot mezglus izveido bez argumentiem. Pēc Build cilnē Node -> Signals parādījās signāls Finished.

2. uzdevums
Abi dalībnieki izstājās tieši vienu reizi. Pēc trešā soda Annai (ApplyPenalty(5)) otrs paziņojums neparādījās, jo metode pārbauda IsActive() un jau izstājušam dalībniekam neko nedara.

3. uzdevums
(a) Secība: vispirms Setup(), tad Finished += ..., tad ApplyPenalty(). Signāli, kas izraidīti pirms pieslēgšanas, vēlāk neatkārtojas.
(b) Pēc -= Annas enerģija tik un tā samazinājās līdz 0, jo atvienošana noņem tikai klausītāju, bet nemaina paša dalībnieka stāvokli. Tāpēc Output bija "Annas enerģija beigās: 0", bet paziņojumu izdrukāja tikai Jānis.
(c) TournamentManager pazīst Participant un pieslēdz tā signālu. Participant tikai izziņo notikumu Finished un nezina, kas to klausās.
3.4. stunda

1. uzdevums

Izveidoju Obstacle klasi ar GlobalClass, lai Godot varētu pievienot šo tipu kā mezglu. Pēc Build to varēju atrast Add Child Node logā.

2. uzdevums

Ar Export pievienoju ObstacleName un Penalty, lai tos varētu mainīt Inspector panelī. Penalty izmantoju set, lai sods nekad nebūtu mazāks par 1. Pārbaudot ar -5, vērtība kļuva par 1.

3. uzdevums

Izveidoju trīs Obstacle mezglus, kuri izmanto vienu un to pašu skriptu, bet katram ir savs nosaukums un sods. Kad nomainīju PrinterQueue sodu no 20 uz 25, pārējie mezgli palika nemainīgi, jo katram mezglam Inspector vērtības tiek saglabātas atsevišķi.

Inspector vērtība ir svarīgāka par kodā norādīto sākuma vērtību, jo tā tiek saglabāta pašā scēnā un tiek izmantota, kad programma tiek palaista.
3.5. stunda

1. uzdevums

Izveidoju Stats struktūru ar Energy, MaxEnergy un Focus. Pārbaudīju vērtību kopēšanu. Izmainot copy.Energy uz 40, original.Energy palika 100, jo struct kopē vērtības neatkarīgi.

2. uzdevums

Pārvietoju dalībnieka enerģiju uz Stats un pievienoju Heal(). Pēc soda 30 Annai palika 70 enerģijas. Pēc Heal(50) enerģija atjaunojās līdz 100, jo tā nevar pārsniegt MaxEnergy. Pārbaudīju, ka GetStats() atgriež kopiju un tās izmaiņas neietekmē Annas īsto enerģiju.

3. uzdevums

Izveidoju Buff struktūru un bonusu sarakstu. Pievienoju Atbalsts +5 un Atpūta +3. Kopējais fokuss bija 18. Atkārtoti izsaucot GetTotalFocus(), rezultāts palika 18. Pēc pirmā bonusa noņemšanas fokuss bija 13.
3.6. stunda — Turnīra simulators

Palaišana

1. Atvērt projektu Godot.
2. Atvērt `Scenes/Tournament.tscn`.
3. Nospiediet F6, lai palaistu turnīru.
4. Turnīra gaita redzama Output panelī.

Secinājumi

Kāpēc izmantojam GetActiveParticipants().Count, nevis party.Count?

party.Count skaita visus dalībniekus, arī tos, kuri jau izstājušies. GetActiveParticipants().Count skaita tikai aktīvos dalībniekus, tāpēc turnīrs beidzas, kad palicis viens aktīvs dalībnieks.

Kāpēc turnīrs garantēti beidzas pirms 1000 kārtām?

Sākumā kopējā enerģija ir 330. Katrā kārtā vismaz viens enerģijas punkts tiek zaudēts, un enerģija netiek atjaunota. Tāpēc turnīrs nevar turpināties bezgalīgi un beidzas pirms 1000 kārtām.

Kā darbojas nejaušā izvēle?

GetActiveParticipants() atlasa tikai aktīvos dalībniekus. GD.RandRange() izvēlas nejaušu dalībnieku un šķērsli. Izstājušies dalībnieki vairs netiek izvēlēti.

Izmantotās klases

Stats — glabā enerģiju un fokusu.
Buff — glabā fokusa bonusus.
Participant — bāzes dalībnieka klase.
Captain, Presenter, TimeKeeper — atvasinātās lomu klases.
Obstacle — glabā šķēršļa nosaukumu un sodu.
TournamentManager — vada turnīru un nosaka uzvarētāju.
