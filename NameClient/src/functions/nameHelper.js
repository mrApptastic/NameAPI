export async function getCategories() {
  const response = await fetch('https://names.tesj.dk/Categories', {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
    },
  });
  return response.json();
}

export async function getNames(
  matches,
  contains,
  startsWith,
  endsWith,
  sex,
  vib,
  maxLength,
  minLength,
  category,
  page,
  take
) {
  let url = 'https://names.tesj.dk/Names';

  if (matches?.length > 0) {
    url += '&matches=' + matches;
  }

  if (contains?.length > 0) {
    url += '&contains=' + contains;
  }

  if (startsWith?.length > 0) {
    url += '&startsWith=' + startsWith;
  }

  if (endsWith?.length > 0) {
    url += '&endsWith=' + endsWith;
  }

  if (sex?.length > 0) {
    url += '&sex=' + sex;
  }

  if (vib) {
    url += '&vib=' + vib;
  }

  if (maxLength) {
    url += '&maxLength=' + maxLength;
  }

  if (minLength) {
    url += '&minLength=' + minLength;
  }

  if (category) {
    url += '&category=' + category;
  }

  if (page) {
    url += '&page=' + page;
  }

  if (take) {
    url += '&take=' + take;
  }

  const response = await fetch(url.replace('&', '?'), {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
    },
  });
  return response.json();
}

export async function suggestNames(
  matches,
  contains,
  startsWith,
  endsWith,
  sex,
  vib,
  maxLength,
  minLength,
  category,
  title,
  surname
) {
  let url = 'https://names.tesj.dk/Names/Suggest';

  if (matches?.length > 0) {
    url += '&matches=' + matches;
  }

  if (contains?.length > 0) {
    url += '&contains=' + contains;
  }

  if (startsWith?.length > 0) {
    url += '&startsWith=' + startsWith;
  }

  if (endsWith?.length > 0) {
    url += '&endsWith=' + endsWith;
  }

  if (sex?.length > 0) {
    url += '&sex=' + sex;
  }

  if (vib) {
    url += '&vib=' + vib;
  }

  if (maxLength) {
    url += '&maxLength=' + maxLength;
  }

  if (minLength) {
    url += '&minLength=' + minLength;
  }

  if (category) {
    url += '&category=' + category;
  }

  if (title === true) {
    url += '&title=' + title;
  }

  if (surname === true) {
    url += '&surname=' + surname;
  }

  const response = await fetch(url.replace('&', '?'), {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
    },
  });
  return response.json();
}

export async function suggestCombos(category) {
  let url = 'https://names.tesj.dk//Names/SuggestCombo';

  if (category) {
    url += '&category=' + category;
  }

  const response = await fetch(url, {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
    },
  });
  return response.json();
}

export async function getVibrations() {
  const response = await fetch('https://names.tesj.dk//Vibrations', {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
    },
  });
  return response.json();
}
