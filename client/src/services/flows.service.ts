import { server, exec } from './api';
import { Flow } from '@/shared/models';

export class FlowsService {
  public static getAll() {
    return exec<Flow[]>(server.get('/flows'));
  }
}
