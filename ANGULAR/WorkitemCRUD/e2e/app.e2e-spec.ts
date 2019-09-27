import { AlencarcrudPage } from './app.po';

describe('alencarcrud App', function() {
  let page: AlencarcrudPage;

  beforeEach(() => {
    page = new AlencarcrudPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
