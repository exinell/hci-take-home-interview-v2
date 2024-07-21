export const sanitizeInput = (input: string): string => {
    return input.trim().replace(/[^\w\s@.]/g, '');
};