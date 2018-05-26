import { server, exec } from './api';
import { Participant } from '@/shared/models';

export class ParticipantsService {
  public static getAll() {
    return exec<Participant[]>(server.get('/participants'));
  }
}
