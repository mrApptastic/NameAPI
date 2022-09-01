export function calculateNameVibration(name) {
    let sum = 0;
    const arr = name.toUpperCase();

    for (let i = 0; i < arr.length; i++) {
        const letter = arr.charAt(i);
        sum += getVibrationByLetter(letter);
    }

    return sum;
  }

export function calculateCharacterSum(str) {
    let sum = 0;
    
    for (let i = 0; i < str.length; i++) {
        const char = str.charAt(i);
        if (!isNaN(char)) {
            const num = parseInt(char);
            sum += num;
        }
    }

    return sum;
}

export function calculateDigitSum(value) {
    let sum = 0;

    while (value) {
        sum += value % 10;
        value = Math.floor(value / 10);
    }

    return sum;
} 

export function getBaseEnergy(energy) {
    switch(energy) {
        case 1: return `1'eren er meget selvstændig og original. Du har gode lederevner og trives med at være den, der bestemmer og sætter dagsordenen. En 1'er Leder i balance vil være god til at motivere og uddelegere arbejdet. Hvorimod en 1'er Leder i ubalance vil påtage sig alt arbejdet selv af frygt for, at andre ikke vil kunne leve op til 1'erens høje standarder. Derved kommer en 1'er i ubalance let til at knokle for meget.

        Som 1'er er du initiativrig og opfindsom. Du elsker at være kreativ og at skabe noget. Du er stærk og har et naturligt positivt syn på livet. Du kan altid få øje på det positive i enhver proces og udfordring du havner i. Du elsker anerkendelse og ros samt at få synlige beviser på din succes. For dig er det at tabe ansigt eller få et nederlag noget af det værste, der kan ske. Derfor kan du også være meget stædig og vedholdende at diskutere med.
        
        Du har ligeledes et stort behov for personlig frihed - dette gælder både i dit arbejdsliv og dit parforhold. Noget af det værste du kan blive udsat for som 1'er er, hvis andre vil bestemme over dig eller trække ting ned over hovedet på dig. Du ønsker selv at bestemme og træffe beslutninger inden for alle livsområder.`;
        case 2: return `2'eren er meget følsom og sensitiv. Du er meget åben i din energi og mærker alt omkring dig dybt og inderligt. Det kan derfor være svært for dig at mærke, hvor grænsen går mellem dig og andre. Du mærker let andres vibration, stemninger og følelser - og bliver meget let påvirket af dette. Hvis du fx går rundt og føler dig ked af det uden nogen egentlig grund, så er der stor chance for, at det er følelse du har påtaget dig fra en anden.

        Fordi du er så sensitiv, så påvirkes dit humør også af månens cyklus. Det betyder, at dit humør kan svinge meget og det er således rigtig vigtigt for dig at være opmærksom på, at du bruger din tid i nærende miljøer og omgivelser.
        
        Du er meget kreativ og idérig. Men idet 2'eren er en meget blid og mild grundenergi, så er der stor chance for, at du ikke får ført så mange af dine idéer og projekter ud i livet. Med mindre du har fået tilført noget handlekraft og styrke via dine navne.`;
        case 3: return `3'eren er meget stærk og kender sit eget værd. 3'eren er den af grund-energierne, der har mest medfødt styrke. Du ved, hvad du indeholder og kan udrette. Derfor stiller du som 3'er meget høje krav til dig selv og accepterer intet mindre end din bedste præstation - hver gang.

        Som 3'er oplever du verden meget sort/hvid, så det er enten succes eller fiasko. Kombinationen af perfektionisme og enten/eller kan være svær at rumme og det kan være svært altid at stille så høje krav til sig selv og andre.
        
        Du kan godt trives som medarbejder og er god til at modtage ordrer, men du vil typisk trives bedst med selv at bestemme og være leder - og du vil være rigtig gode i den rolle.
        
        Du har let ved at tage et pokeransigt på og skjule dine følelser over for andre. Dette fungerer som en slags skjold mod omverden og du er således i stand til at lukke helt af for dine følelser (i hvert fald i et stykke tid). Dette kan hjælpe dig igennem kriser og når du skal præstere, fx arbejdsmæssigt.`;
        case 4: return `Som 4'er er du meget følsom og blid samtidighed med, at du rummer en enorm overlevelsesevne og kan overkomme de mest utrolige ting. Som 4'er er du det vi kalder en gammel sjæl. Det vil sige, at du har været her mange gange og at du har noget karmisk med, som du skal udleve og balancere i dit liv som 4'er. Du er et skæbnebarn og derfor vil der være mange kontraster i dit liv. Når det går dig godt, så går det helt fantastisk og når det går skidt, så er det rigtig slemt.

        Du kan let føle dig misforstået og måske endda have en følelse af at være lidt forkert og ensom. Selv i selskab med andre vil du kunne have en indre følelse af, at du ikke hører til og er uden for. Du har ofte få venner og er ekstrem loyal over for disse.
        
        Du er meget innovativ og god til at tænke ud af boksen. Du er nysgerrig på livet og elsker at udforske andre måder at gøre tingene på. Du ser altid tingene fra en anden vinkel og er lidt af en ”Ole opfinder”.`;
        case 5: return `Som 5'er er du meget nysgerrig og energisk. Du er altid i udvikling og i gang med noget og gerne flere ting ad gangen. Du er meget social og er som regel med vellidt. Du kan med alle og har let ved at få nye venner og bekendtskaber, så dit netværk er stort og bredt.

        Mange 5'ere arbejder med kommunikation, formidling eller salg. Du er meget velformuleret og taler meget. Og din begejstring og energi vil smitte af og virke motiverende på andre. Du trives ofte med at være i centrum og er ofte den, der taler højest og underholder i selskab med andre. 
        
        Du elsker eventyr og at opleve nye spændende ting. Derfor kaster du dig også ofte ud i nye ting rent impulsivt. Hvis noget skulle mislykkes, så tager du det ikke så tungt. Der er en lethed i din tilgang til livet. Du prøver bare igen eller gør noget andet.
        
        Mentalt er du i gang hele tiden og vil være med til det hele. Din vibration bliver let rastløs og selvom det er umuligt at nå at være med til alt, så har du meget svært ved at vælge noget fra. Det er vigtigt at være opmærksom på, at denne overaktivitet ikke belaster dit nervesystem og fører til stress. Søvn er meget vigtig for dit velbefindende og du bør ligeledes have fokus på at grounde dig selv.`;
        case 6: return `Som 6'er er du meget kærlig og omsorgsfuld. Du er moderlig/faderlig af natur og det falder dig meget naturligt at drage omsorg for andre. Du sætter oftest andres behov før dine egne og er meget fleksibel og altid parat til at hjælpe andre. Du ofrer dig gerne for dem du elsker. Dette store hjælpe- og omsorgsgen er en gave, som du skal bruge her i livet. Men det bliver også let din akilleshæl, fordi du ofte kommer til at overskride dine egne grænser og glemmer at lytte til egne behov. Det er derfor vigtigt for dig at lære, at du skal at være noget for dig selv, for herved at kunne være der for andre - ellers vil du brænde ud pga. dit hjælper-gen.

        Du har en magnetisk udstråling og virker tiltrækkende på mennesker omkring dig. Din blide og kærlige venusvibration virker beroligende og harmoniserende på andre.
        
        Som 6'er er du også meget æstetisk. Du elsker at skabe og opholde dig i smukke og nærende omgivelser. Du har en evne til at få noget til at ligne en million med ganske få midler. Du er lidt at en redebygger og værner meget om din familie og dit hjem. Du elsker at være hjemme og hygge med dem du har kær.`;
        case 7: return `Som 7'er er du meget blid og sensitiv. Rent numerologisk kaldes 7'er energien for ”telefonnummeret til himlen”. Dette fordi, at 7'ere der står rent i sin essens har en åben og helt ren forbindelse til ”kilden”, hvorfor de også har en helt særlig kreativitet. Det kan dog være svært for dig selv at anerkende din intuition og kreativitet som noget særligt. For dig er det helt normalt og mange 7'ere har en tilgang om, at ”det er da ikke noget særligt, det kan alle vel…”, men det kan alle ikke. Ingen andre grundenergier har den form for kreativitet som 7'ere har, den kommer fra et helt særligt sted. Derfor er der også mange 7'ere, der arbejder i kreative fag som fx kunstnere, musikere, sangskriver, forfattere mv.

        Der skal ikke ret meget til for at skabe ubalance i en 7'ers meget sensitive system. Derfor døjer mange 7'ere med sart hud, allergier, madintolerancer el.lign. Du har ikke så megen medfødt styrke/handlekraft. Så hvis du ikke har dette tilført via dine navne, så kan det være svært for dig at få alle dine idéer planer ført ud i livet.
        
        Som 7'er er du oftest overvejende introvert og lader derfor bedst op, når du er alene. Du har et enormt rigt indre liv og har derfor ikke brug for at blive stimuleret ude fra. Du foretrækker typisk mindre doser af sociale sammenkomster og også meget gerne med få ad gangen.`;
        case 8: return `Som 8'er er du meget stærk og passioneret. Du har en medfødt råstyrke ud over det sædvanlige og et massivt overlevelsessystem, der altid driver dig videre og fremad. Du er det, vi kalder en gammel sjæl. Det vil sige, at du har været her mange gange og at du har noget karmisk med, som du skal udleve og balancere i dit liv som 8'er. Der er mange kontraster i dit liv. Når det går dig godt, så går det helt fantastisk og når det går skidt, så er det rigtig slemt. Derfor er du også udstyret med dette helt særlige overlevelsessystem, som hjælper dig med at bearbejde og hurtigt komme videre.

        Du ved inderst inde godt, at livet kan være kontrastfyldt og at du kan miste og må klare dig igennem nogle hårde processer i livet. Derfor kan du komme til at sætte en usynlig mur op omkring dig, for at beskytte dig selv. Det gør, at du kan være lidt skeptisk over for nye mennesker og typisk ser andre grundigt an, før du lukker dem ind i dit liv.
        
        Din selvfølelse kan svinge mellem at føle dig helt unik & fantastisk til misforstået & forkert. Mange 8'ere har følelsen af at være ”det sorte får” i flere perioder af deres liv. Og det kan give en følelse af at være ensom og alene i verden - selv når du er i selskab med andre.
        
        Du er meget dyb og intens af natur. Du har et varmt og godt hjerte, der banker for at andre har det godt.`;
        default: return `Som 9'er er du meget hårdtarbejdende og viljestærk. Du kan være meget direkte i din kommunikation, fordi du som 9'ere ikke har lært at pakke tingene ind. Derfor findes der heller ingen skjult agenda, når du kommunikerer. Du siger, hvad du mener uden, at der ligger noget mellem linjerne.

        Du er dygtig til at organisere og strukturere. Men du trives bedst, hvis du også har kontrollen - ellers mister du let interessen. Hvis du som 9'er har rod, uorden eller ikke er struktureret, så er det et tydeligt tegn på, at du er ude af balance.
        
        Du er født til at være leder og have styringen selv. Og er du i balance, så vil du kunne udøve både humanistisk og diplomatisk ledelse med respekt og empati for dine medarbejdere.
        
        Selvom du som 9'er fremstår ekstrem stærk på dine omgivelser, så er du meget følsom inden i og vil gøre meget for kærlighed og dem du elsker.
        
        9'ere har det ofte svært i sine unge år. Men de opnår som regel altid succes senere i livet pga. deres styrke, vilje og beslutsomhed.`;
    }
}

function getVibrationByLetter(letter) {
    switch (letter) {
        case 'A': return 1;
        case 'B': return 2;
        case 'C': return 3;
        case 'D': return 4;
        case 'E': return 5;
        case 'F': return 8;  
        case 'G': return 3;
        case 'H': return 5;
        case 'I': return 1;
        case 'J': return 1;
        case 'K': return 2;
        case 'L': return 3;
        case 'M': return 4;
        case 'N': return 5;
        case 'O': return 7;
        case 'P': return 8;
        case 'Q': return 1;
        case 'R': return 2;  
        case 'S': return 3;
        case 'T': return 4;
        case 'U': return 6;
        case 'V': return 6;
        case 'X': return 5;
        case 'Y': return 1;     
        case 'Z': return 7;
        case 'Æ': return 6;
        case 'Ø': return 7;
        case 'Å': return 1;                                   
        default: return  0;
    }
}
