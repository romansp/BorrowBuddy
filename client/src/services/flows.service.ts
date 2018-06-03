import { server, exec } from './api';
import { Flow, FlowPost } from '@/shared/models';

export class FlowsService {
  public static getAll() {
    return exec<Flow[]>(server.get('/flows'));
  }

  public static post(flow: FlowPost) {
    return exec(server.post('/flows', flow));
  }
}
