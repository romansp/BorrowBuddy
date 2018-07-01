export function cloneDeep<T>(obj: any): T {
  return JSON.parse(JSON.stringify(obj));
}
