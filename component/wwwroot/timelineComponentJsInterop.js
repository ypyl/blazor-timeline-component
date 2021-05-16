export const getBoundingClient = (element) => {
  const rect = element.getBoundingClientRect();
  return [rect.top, rect.bottom];
};
export const getClientHeight = (element) => element.clientHeight;
export const setTranslateY = (element, value) =>
  (element.style.transform = 'translateY(' + value + 'px)');
export const setVisibility = (element, value) =>
  (element.style.visibility = value ? 'visible' : 'hidden');
