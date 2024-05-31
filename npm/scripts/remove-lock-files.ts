import fse from 'fs-extra';
import { log } from './utils/log';

removeLockFiles();

export async function removeLockFiles() {
  const folders = [
    '../../templates/app/react-native'
  ];

  try {
    for (let i = 0; i < folders.length; i++) {
      await fse.remove(`${folders[i]}/yarn.lock`);
      await fse.remove(`${folders[i]}/package-lock.json`);
    }
  } catch (error) {
    throwError(error?.message || error);
  }
}

function throwError(error: string) {
  log.error(error);
  process.exit(1);
}
