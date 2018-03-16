﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;
using MathService.Services.Contracts;
using MathService.Models.EulerModels;
using MathService.Models;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace MathService.Services.Implementations
{
    public partial class EulerService : IEulerService
    {
        //https://projecteuler.net/problem=500
        //Problem 500!!!
        //Problem 500 Published on Saturday, 31st January 2015, 01:00 pm; Solved by 2549; Difficulty rating: 15%
        //        
        //The number of divisors of 120 is 16.
        //In fact 120 is the smallest number having 16 divisors.

        //Find the smallest number with 2^500500 divisors.
        //Give your answer modulo 500500507.

        //-------- 120
        //1 2 3 4 5 6 8 10 12 15 20 24 30 36 60 120

        //120 = 2^3 * 3^1 * 5^1
        //(3+1)(1+1)(1+1) = 16

        // all even numbers with exactly 16 factors
        //2^15                  = 32768
        //2^7 * 3^1             = 348
        //2^3 * 3^3             = 216
        //2^3 * 3^1 * 5^1       = 120
        //2^1 * 3^1 * 5^1 * 7^1 = 210
        //---------------------------------------------

        //------------ 32 factors       
        //2147483648 	= 2^31						
        //98304 		= 2^15 * 3^1
        //1152			= 2^7 * 3^3
        //640			= 2^7 * 3^1 * 5^1
        //1080			= 2^3 * 3^3 * 5^1
        //840			= 2^3 * 3^1 * 5^1 * 7^1
        //2310			= 2^1 * 3^1 * 5^1 * 7^1 * 11^1




        public object RunProblem500(int maxFactors)
        {
            var level_500500 = ReadTextFile();

            var level = new List<int> { 2, 1, 1 };
            //var levelFactorCount = GetLevelFactorCount(result);
            for(var i = 5; i <= maxFactors; i++)
            {
                level = GetNextLevel(level);
                //Print(result);

                
            }



            //return new { result = level_500500, sum = level_500500.Sum() };

            return BigInteger.Remainder(GetNumberFromExpansion(level, _mod), _mod).ToString();
        }

        private int GetLevelFactorCount(List<int> level)
        {
            var factorCount = 0;
            for (var i = 0; i < level.Count; i++)
                factorCount += _levelPowers[level[i]];
            return factorCount;
        }

        private BigInteger FindNumberOfDivisors(BigInteger num)
        {
            var n = BigInteger.Zero;


            return n;
        }

        private BigInteger GetNumberFromPrimeFactorization(List<int> powers)
        {
            var num = BigInteger.One;
            var primes = _calc.GetFirstNPrimes(powers.Count);

            for (var i = 0; i < powers.Count; i++)
                for (var p = 0; p < powers[i]; p++)
                    num *= primes[i];


            return num;
        }

        private BigInteger GetNumberFromExpansion(List<int> levels)
        {
            var num = BigInteger.One;

            var primes = _calc.GetFirstNPrimes(levels.Count);

            for (var i = 0; i < levels.Count; i++)
            {
                num *= BigInteger.Pow(primes[i], _levelPowers[levels[i]]);//.ModPow(primes[i], _levelPowers[levels[i]], _mod);
            }
            return num;
        }

        private BigInteger GetNumberFromExpansion(List<int> levels, BigInteger mod)
        {
            var num = BigInteger.One;

            var primes = _calc.GetFirstNPrimes(levels.Count);

            for (var i = 0; i < levels.Count; i++)
            {
                num *= BigInteger.ModPow(primes[i], _levelPowers[levels[i]], mod);
            }
            return num;
        }

        private List<int> GetNextLevel(List<int> currLevel)
        {
            // level increase is prime^(2^current level value)
            var nextLevel = new List<int>(currLevel);
            var leastIncr = toTwoPower((long)_calc.GetPrime(0), currLevel[0]);
            var incrPos = 0;

            for (var i = 1; i < currLevel.Count; i++)
            {
                // on same level so can't beat previous result
                if (currLevel[i] == currLevel[i - 1])
                    continue;
                
                //var incr = BigInteger.Pow((long)_calc.GetPrime(i), _twoPowers[currLevel[i]]);
                var incr = toTwoPower((long)_calc.GetPrime(i), currLevel[i]);
                
                if (incr < leastIncr)
                {
                    leastIncr = incr;
                    incrPos = i;
                }

                // break out after first level 1
                if (currLevel[i] == 1)
                    break;
            }

            // check vs next new prime
            var nextPrime = _calc.GetPrime(currLevel.Count);
            if (nextPrime < leastIncr)
                nextLevel.Add(1);
            else
                nextLevel[incrPos]++;


            return nextLevel;
        }

        private void expProgression()
        {
            var str = "new List<ulong>(){ \"1\", \"2\" ";
            var num = new BigInteger(2);
            
            for(var i = 0; i < 15; i++)
            {
                num *= num;
                str += $",\n \"{num}\"";
            }
            
            str += " };";
            Debug.WriteLine(str);
        }

        private static BigInteger _mod = BigInteger.Parse("500500507");
        private static List<int> _levelPowers = new List<int> { 0, 1, 3, 7, 15, 31, 63, 127, 255, 511, 1023 };
        private static List<ulong> _twoPowers = new List<ulong>(){ 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768, 65536, 131072, 262144, 524288, 1048576, 2097152, 4194304, 8388608, 16777216, 33554432, 67108864,
            134217728, 268435456, 536870912, 1073741824, 2147483648, 4294967296, 8589934592, 17179869184, 34359738368, 68719476736, 137438953472, 274877906944, 549755813888, 1099511627776, 2199023255552, 4398046511104, 8796093022208, 17592186044416, 35184372088832,
            70368744177664, 140737488355328, 281474976710656, 562949953421312, 1125899906842624, 2251799813685248, 4503599627370496, 9007199254740992, 18014398509481984, 36028797018963968, 72057594037927936, 144115188075855872, 288230376151711744, 576460752303423488,
            1152921504606846976, 2305843009213693952, 4611686018427387904, 9223372036854775808 };
        private static List<string> _twoLevels = new List<string>()
        {
            "2","4","16","256","65536","4294967296","18446744073709551616","340282366920938463463374607431768211456","115792089237316195423570985008687907853269984665640564039457584007913129639936",
            "13407807929942597099574024998205846127479365820592393377723561443721764030073546976801874298166903427690031858186486050853753882811946569946433649006084096",
            "179769313486231590772930519078902473361797697894230657273430081157732675805500963132708477322407536021120113879871393357658789768814416622492847430639474124377767893424865485276302219601246094119453082952085005768838150682342462881473913110540827237163350510684586298239947245938479716304835356329624224137216",
            "32317006071311007300714876688669951960444102669715484032130345427524655138867890893197201411522913463688717960921898019494119559150490921095088152386448283120630877367300996091750197750389652106796057638384067568276792218642619756161838094338476170470581645852036305042887575891541065808607552399123930385521914333389668342420684974786564569494856176035326322058077805659331026192708460314150258592864177116725943603718461857357598351152301645904403697613233287231227125684710820209725157101726931323469678542580656697935045997268352998638215525166389437335543602135433229604645318478604952148193555853611059596230656",
            "1044388881413152506691752710716624382579964249047383780384233483283953907971557456848826811934997558340890106714439262837987573438185793607263236087851365277945956976543709998340361590134383718314428070011855946226376318839397712745672334684344586617496807908705803704071284048740118609114467977783598029006686938976881787785946905630190260940599579453432823469303026696443059025015972399867714215541693835559885291486318237914434496734087811872639496475100189041349008417061675093668333850551032972088269550769983616369411933015213796825837188091833656751221318492846368125550225998300412344784862595674492194617023806505913245610825731835380087608622102834270197698202313169017678006675195485079921636419370285375124784014907159135459982790513399611551794271106831134090584272884279791554849782954323534517065223269061394905987693002122963395687782878948440616007412945674919823050571642377154816321380631045902916136926708342856440730447899971901781465763473223850267253059899795996090799469201774624817718449867455659250178329070473119433165550807568221846571746373296884912819520317457002440926616910874148385078411929804522981857338977648103126085903001302413467189726673216491511131602920781738033436090243804708340403154190336",
            "1090748135619415929462984244733782862448264161996232692431832786189721331849119295216264234525201987223957291796157025273109870820177184063610979765077554799078906298842192989538609825228048205159696851613591638196771886542609324560121290553901886301017900252535799917200010079600026535836800905297805880952350501630195475653911005312364560014847426035293551245843928918752768696279344088055617515694349945406677825140814900616105920256438504578013326493565836047242407382442812245131517757519164899226365743722432277368075027627883045206501792761700945699168497257879683851737049996900961120515655050115561271491492515342105748966629547032786321505730828430221664970324396138635251626409516168005427623435996308921691446181187406395310665404885739434832877428167407495370993511868756359970390117021823616749458620969857006263612082706715408157066575137281027022310927564910276759160520878304632411049364568754920967322982459184763427383790272448438018526977764941072715611580434690827459339991961414242741410599117426060556483763756314527611362658628383368621157993638020878537675545336789915694234433955666315070087213535470255670312004130725495834508357439653828936077080978550578912967907352780054935621561090795845172954115972927479877527738560008204118558930004777748727761853813510493840581861598652211605960308356405941821189714037868726219481498727603653616298856174822413033485438785324024751419417183012281078209729303537372804574372095228703622776363945290869806258422355148507571039619387449629866808188769662815778153079393179093143648340761738581819563002994422790754955061288818308430079648693232179158765918035565216157115402992120276155607873107937477466841528362987708699450152031231862594203085693838944657061346236704234026821102958954951197087076546186622796294536451620756509351018906023773821539532776208676978589731966330308893304665169436185078350641568336944530051437491311298834367265238595404904273455928723949525227184617404367854754610474377019768025576605881038077270707717942221977090385438585844095492116099852538903974655703943973086090930596963360767529964938414598185705963754561497355827813623833288906309004288017321424808663962671333528009232758350873059614118723781422101460198615747386855096896089189180441339558524822867541113212638793675567650340362970031930023397828465318547238244232028015189689660418822976000815437610652254270163595650875433851147123214227266605403581781469090806576468950587661997186505665475715792896",
            "1189731495357231765085759326628007130763444687096510237472674821233261358180483686904488595472612039915115437484839309258897667381308687426274524698341565006080871634366004897522143251619531446845952345709482135847036647464830984784714280967845614138476044338404886122905286855313236158695999885790106357018120815363320780964323712757164290613406875202417365323950267880089067517372270610835647545755780793431622213451903817859630690311343850657539360649645193283178291767658965405285113556134369793281725888015908414675289832538063419234888599898980623114025121674472051872439321323198402942705341366951274739014593816898288994445173400364617928377138074411345791848573595077170437644191743889644885377684738322240608239079061399475675334739784016491742621485229014847672335977897158397334226349734811441653077758250988926030894789604676153104257260141806823027588003441951455327701598071281589597169413965608439504983171255062282026626200048042149808200002060993433681237623857880627479727072877482838438705048034164633337013385405998040701908662387301605018188262573723766279240798931717708807901740265407930976419648877869604017517691938687988088008944251258826969688364194133945780157844364946052713655454906327187428531895100278695119323496808703630436193927592692344820812834297364478686862064169042458555136532055050508189891866846863799917647547291371573500701015197559097453040033031520683518216494195636696077748110598284901343611469214274121810495077979275556645164983850062051066517084647369464036640569339464837172183352956873912042640003611618789278195710052094562761306703551840330110645101995435167626688669627763820604342480357906415354212732946756073006907088870496125050068156659252761297664065498347492661798824062312210409274584565587264846417650160123175874034726261957289081466197651553830744424709698634753627770356227126145052549125229448040149114795681359875968512808575244271871455454084894986155020794806980939215658055319165641681105966454159951476908583129721503298816585142073061480888021769818338417129396878371459575846052583142928447249703698548125295775920936450022651427249949580708203966082847550921891152133321048011973883636577825533325988852156325439335021315312134081390451021255363707903495916963125924201167877190108935255914539488216897117943269373608639074472792751116715127106396425081353553137213552890539802602978645319795100976432939091924660228878912900654210118287298298707382159717184569540515403029173307292454391789568674219640761451173600617752186991913366837033887201582071625868247133104513315097274713442728340606642890406496636104443217752811227470029162858093727701049646499540220983981932786613204254226464243689610107429923197638681545837561773535568984536053627234424277105760924864023781629665526314910906960488073475217005121136311870439925762508666032566213750416695719919674223210606724721373471234021613540712188239909701971943944347480314217903886317767779921539892177334344368907550318800833546852344370327089284147501640589448482001254237386680074457341910933774891959681016516069106149905572425810895586938833067490204900368624166301968553005687040285095450484840073528643826570403767157286512380255109954518857013476588189300004138849715883139866071547574816476727635116435462804401112711392529180570794193422686818353212799068972247697191474268157912195973794192807298886952361100880264258801320928040011928153970801130741339550003299015924978259936974358726286143980520112454369271114083747919007803406596321353417004068869443405472140675963640997405009225803505672726465095506267339268892424364561897661906898424186770491035344080399248327097911712881140170384182058601614758284200750183500329358499691864066590539660709069537381601887679046657759654588001937117771344698326428792622894338016112445533539447087462049763409147542099248815521395929388007711172017894897793706604273480985161028815458787911160979113422433557549170905442026397275695283207305331845419990749347810524006194197200591652147867193696254337864981603833146354201700628817947177518115217674352016511172347727727075220056177748218928597158346744541337107358427757919660562583883823262178961691787226118865632764934288772405859754877759869235530653929937901193611669007472354746360764601872442031379944139824366828698790212922996174192728625891720057612509349100482545964152046477925114446500732164109099345259799455690095576788686397487061948854749024863607921857834205793797188834779656273479112388585706424836379072355410286787018527401653934219888361061949671961055068686961468019035629749424086587195041004404915266476272761070511568387063401264136517237211409916458796347624949215904533937210937520465798300175408017538862312719042361037129338896586028150046596078872444365564480545689033575955702988396719744528212984142578483954005084264327730840985420021409069485412320805268520094146798876110414583170390473982488899228091818213934288295679717369943152460447027290669964066816",
            "1415461031044954789001553027744951601348130711472388167234385748272366634240845253596025356476648415075475872961656126492389808579544737848881938296250873191743927793544913011050162651277957029846960211783242933521207545413484969856851851141288515163201482995389055097460622098635675003353929224278582935664416262572773308153277514346480313371988612629481483562438178928958867777850072198316174841251955590996672018645093640850803679630220367201383844866791449284737518262813123083439037243678440420897139923778278952770312318778329004894547065489077596835396017153603170050371302014762443872701111379554484309718662306883776010475348441493600491943479041271992920195331983064930106164727241438940877685164658948654886171641124473975626241632750150126655369981021293570066042305482486040883165635862835728637046058352403756085745691239473897891999085976345203704659967157427239535836507133656908815246080139195569461072006301590372954830738644391138016065344131131207604264053897440828904662047183234377547427287691941741535946510882990904477863185473798528388060457568927943633923928872681927502029572963130840854853739937076881035646179383055483433876051402037614424748902969018159186519811051545367967103767182819709135479019131683309330797374408197339831527239040715908112213095126770717606001288898889370710896248862361503869205214536908258196921765593065325392836332142594411134603475509366028145690306350601859295261296263331018682276317567749534571058772235567679556920240789109070521253987131031263902293034744367356932509952188828475362311316445284228640489421809263738423630931243024914587863928134719186104164660605356001591962778646378295413659770782646979236289062616442418071571039282551289348848274522893059561717860194034698241804887531275078109603637160495907579995366419636417028927573391670580796818526074226095014375189438579216071677540766085605604106123036669667434677723472675564458991671268414100801031453917736665947284956674884035306621286465183798693852599803324619865181856244422079233687294508536408521087418873908498202710597080474480249818858011490930518512713798803629101638716278578874145214429026186276602289012484526830076647356828764878327726719816428678904207944456894393185983090347045176886732326253912297649524439880403701430566761380359925558522718201954287517587367247510777678934664472548647787048306330770862370015525858005479756471499227244901142805749769564184753211967223226212964165678856604892416984915097422960534122333453876981279243565766391796311605149222628328255333061543877525846029404507128890053189442527544665141513571136187126874914601669750392486100507568442046831780631032540574077944274454228087639241736818505163759910365131990632947462993204585218122431992323864244947394390438656356424037471932484452186545692102503479070599953823165194211019696760575264426802848303031804305733532228050180286034175168918827460626042268658295140706915047189049492578996650494005882337071150004868956193406593184838633694068409823903437144417376039174235011053228846851424217955172902918651003610984108402650929359396305634313047088725113039076811940090109855578285937829164213576612822210347957745947331047482525346602542653176899809278808232796557531832150249769253600667902268029661700149632868541956266119528042486534014778779846981761033155007262730162675954520221887384710387051721829271917592295057169589539706361671082094809405871790468623234895914796464390019259167513771864832869036015645542195008456098615038348028400803005801944957156282746379539470250603755465358786281476086044567893715130635914946085361612372742732638037372287633897118303250036355897758209569010246051563429109255864938245524550200580218274314809738075022775651220374105227215906205292751918667060475328593225367793061070422108733800983855507591806413460964585595316359971179284283604731468668545484761381747591639736453419896449323486397030776397761202590467637505473390222249456724093238682557762781839530938233751281999608711356147835655195236666056355678841889862284014674059052995170220711404445012766642203314592371712594877968343265210232798135023299117318191770365123807086704381809759602260151612996896994294186084475619138121455294389585874237791634701296124550179672059485838256445846530599137662480841344376503989244633345016070887198120421435575726237189312161818021548006389501182393441712142044953072264016676799011624620312246468554654371544717355227740157629086739710675845209992133342035144038961065892653392182875622932670067798433934891709519877850794219491447988160171932331006495620280094149464379450153085406225081471879585894087916092141623752345112751067703166403681162331920291740847388957632311053342426152947324011627922225878539935022974616062774839110489080094174972841068102006645677499293769091362853719300958775222088670909723895414866464400756314470281962034276531512544009726174649399937581739718117982417360985958259468485436586733686659771030677664677905401522360041892481951454135360540917411098412286723830672712910603386748136348878056545746112142111116659985782398275627122449414307140749404884060753863706024281031485538666133332835597817514616331728118982180628823657668668099621799842011158020767772792912298768785614574416019320546196793486018677884550715870608004988829978148966980770136884353226949853654541655837029498017031085943660440466076019479435181045436897845351048788440102268677798257134774081674357195358847761226603653040273985441982221318087815835107317571211841215825606453348521097936413520295998260751040778703740366309871999114717441069182825782996299411623087415008910941303284182131584799816210728034255555687956785287887098941927737599159824398527573423117728252839768919145018117995597946282264946523741691180858441194373387098969530126834014923656627849129062189145572870212225909464530919531634090206371146321663797279805760028495288084848179380053931435604182517516600823936737919915974103591167638898871754018470172589234989397051893028574365288890303780239194622538215532212735555299397542845350629361560347195459286729561168235383428930828378887678197590624110372267338756668414899526138480866747114831414687579691636020586013635675559033488649721062244142787960404056961306176022981242840984825421797150393909713884990708008654327558025619074213270714252431515171638994505720144347502185848528129000055629703422695425047209518177577128511315492059613722366724615343968357332924191178041956753132941596322101250417068813977303498033940613791099720174786773844696734034010269139553878212387108508158622839733593634918975616613368243882808391509244806809337882965338701545504691589517650472239580754189547649790038026506707709763098148746524031013800531890876858227360501676161874661946431647929572555992967298553471152693471452480119897608173336753260124452510815333416409357029633265172614203663326409976153102467083379129588112837280711111346490898947808402062401228963194162711657422998429263579276515414000979053740662733816632981508535128518440258006782392288860903823207436820793041881907093791874917635829854808150640894907127600430185202301457398493533111918994100853753771956293430162566011361163168953462188010231819000213669780447856871248564377584509085888797883747538309181810700615032225563091016689474780253412571338625581817921061549352557588323659562675650563535186144754433556539747098322643609542486120512917463706713059064230762924353617972053492527119192658171851345957700772571841998176036269399889012480745075924980479390349332734102774400008120073461540540571051506971003904623484344762005126345905465893817895377928139596549936720100490013375956605889200767596688065961076003472005585890222594655010430123127351665316862663484785966446263740624564875351640780923703006868107440790271682936043304873579648989978571754898840789184480129570446991029504758616393106487523321312507454036808667210073602144290050619610614738139543285649346986393213102456863793665512533655931941790537619020604730601443181250662019687491711547360597117856977240817110039737153355212454745907787131179578656470567535050192702347739653312290522505871612897990429961857550031033665725647987982751868407294730437319505335782425290324427994308374960806684435416445907817107168298342945182985594111281499472495749501642418042734681158560450241309279063816746067016274795461832774699007774423895748677615395848493765672271957278664546343161609126810028254214659633625223934508931691728584028342356669332635458079957147833789501696835781748234884919093574276330354577841975249603899105407104895787598263630679206627796433754045597409176502438867600537016879169914474754840024311922169647388379952802523847080102439285112565751677726373201688403254466789070713575214256192452449029110040537994627424532403620179880056518721219108771374073638118719073211367295514376708596537294204544099903590754371656178656960170217675832153268164591674004500239073743502648099105624548587493544825479270589742494495181223722329693345468639306990523118995640567042531369577131113159027381808071346991604513906450398219426300899204662435492578485666553974492723173859777485412927016068341515517437115137050372750279278548332799193799400154166442801427310076537020119936073495221974791939759846168942055397357348278178050441024490005893818256958665388971201322931680573846084525016332224646866617490832496084991690171583029352609836490908113464668343829985351590552945284372573978295485097950893072423518605982085487154470155025269025854044646496572448495225343096261896378931482883286657508859228868487844184479087538453974144187004884366561256723404772190847586008543961349071131191708943295337664384184532774661503245910257451923515607711559705478132556040197663414911885143697985464807309297002910250505302287769412337441199360424519160716552890882816796378296881641827981453035207233752063518782778493743827708109913493162932182427627261046280168269958077541122668104633712377856"
        };

        private static BigInteger toTwoPower(long x, int y)
        {
            long result = x;
            ulong i = 2;
            while (i <= _twoPowers[y])
            {
                result *= x;
                i++;
            }
            return result;
        }

        private static BigInteger toTwoDoublePower(long x)
        {
            var result = BigInteger.One;



            return result;
        }

        private void Print(List<int> list)
        {
            var str = $"{{{list[0]}";

            for (var i = 1; i < list.Count; i++)
            {
                str += $", {list[i]}";
            }

            str += $"}}";
            Debug.WriteLine(str);
        }

        private List<int> ReadTextFile()
        {
            var intList = new List<int>(
            File.ReadLines("../MathService/Repositories/Constants/euler_500_answer.txt")
                //.AsParallel() //maybe?
                .SelectMany(line => Regex.Matches(line, @"\d+").Cast<Match>())                
                .Select(i => int.Parse(i.Value)));
            ;
            return intList;
        }
    }
}
