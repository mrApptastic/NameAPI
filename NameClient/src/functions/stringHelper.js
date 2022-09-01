export function formatName(fullName) {
    if (!fullName) {
      return '';
    }
  
    const names = fullName.split(' ');
    const formattedNames = new Array();
  
    for (const name of names) {
      formattedNames.push(
        name.substring(0, 1).toUpperCase() + name.substring(1).toLowerCase()
      );
    }
  
    return formattedNames.join(' ');
  }