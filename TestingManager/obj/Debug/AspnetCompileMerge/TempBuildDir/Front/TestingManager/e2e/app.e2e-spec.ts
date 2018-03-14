import { TestingManagerPage } from './app.po';

describe('testing-manager App', function() {
  let page: TestingManagerPage;

  beforeEach(() => {
    page = new TestingManagerPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
