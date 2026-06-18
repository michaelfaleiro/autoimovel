window.ApplyMonetaryMask = (input) => {
    let value = input.value.replace(/[^\d,]/g, '');
    let parts = value.split(',');
    if (parts.length > 2) {
        value = parts[0] + ',' + parts.slice(1).join('');
        parts = value.split(',');
    }
    if (parts.length === 2 && parts[1].length > 2) {
        parts[1] = parts[1].substring(0, 2);
        value = parts[0] + ',' + parts[1];
    }
    let intPart = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, '.');
    input.value = parts.length === 2 ? intPart + ',' + parts[1] : intPart;
};

window.InitMonetaryInput = (input) => {
    input.addEventListener('input', () => window.ApplyMonetaryMask(input));
};
