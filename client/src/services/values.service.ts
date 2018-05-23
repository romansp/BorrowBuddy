import { server, exec } from './api';

export interface IValuesData extends Array<string> {}

export class ValuesService {
  public static getValues() {
    return exec<IValuesData>(server.get('/values'));
  }
}
