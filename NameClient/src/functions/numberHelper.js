export function calculateNameVibration(name) {
    let sum = 0;
    const arr = name.toUpperCase();

    for (let i = 0; i < arr.length; i++) {
        const letter = arr.charAt(i);
        console.log(letter);
        sum += getVibrationByLetter(letter);
    }
    return sum;
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
