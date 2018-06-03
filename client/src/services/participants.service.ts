import { server, exec } from './api';
import { Participant } from '@/shared/models';

export default class ParticipantsService {
  public static getAll() {
    return exec<Participant[]>(server.get('/participants'));
  }
}
